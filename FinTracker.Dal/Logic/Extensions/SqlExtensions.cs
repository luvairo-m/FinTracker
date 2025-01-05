using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using FinTracker.Dal.Logic.Attributes;
using FinTracker.Dal.Logic.Utils;

namespace FinTracker.Dal.Logic.Extensions;

internal static class SqlExtensions
{
    private const char defaultParamPrefix = '@';
    private const string defaultValueTemplate = "{0}";
    
    private static readonly ConcurrentDictionary<Type, PropertyInfo[]> TypeProperties = new();
    
    public static string GetTableName(this Type tableType)
    {
        var tableMeta = GetTableMeta(tableType);
        return $"{tableMeta.schema}.{tableMeta.name}";
    }
    
    public static IEnumerable<string> GetColumnNames(this Type type, bool withKeys = true)
    {
        return GetProperties(type)
            .WithoutIgnored(withKeys: withKeys)
            .Select(prop => prop.GetCustomAttribute<ColumnAttribute>()?.Name ?? prop.Name);
    }

    public static IEnumerable<string> GetParameterNames(this Type type, char paramPrefix = '@', bool withKeys = true)
    {
        return GetProperties(type)
            .WithoutIgnored(withKeys: withKeys)
            .Select(prop => $"{paramPrefix}{prop.Name}");
    }
    
    public static string ToWhereExpression<T>(this T sqlModel, bool skipNull = true)
    {
        return CreateExpressionFromModel(
            sqlModel,
            include: _ => true,
            operatorSelector: prop => prop.PropertyType == typeof(string)
                ? "LIKE"
                : prop.PropertyType.IsAssignableTo(typeof(IEnumerable))
                    ? "IN"
                    : "=",
            conditionLinker: conditions => SqlRequestUtils.BuildWhereExpression(conditions),
            paramPrefix: defaultParamPrefix,
            skipNull: skipNull);
    }
    
    public static string ToSetExpression<T>(this T sqlModel, bool skipNull = true)
    {
        return CreateExpressionFromModel(
            sqlModel,
            include: prop => prop.GetCustomAttribute<ReadOnlyAttribute>() is not { IsReadOnly: true },
            operatorSelector: _ => "=", 
            conditionLinker: SqlRequestUtils.BuildSetExpression, 
            paramPrefix: defaultParamPrefix,
            skipNull: skipNull);
    }

    public static Dictionary<string, object> ToParametersWithValues<T>(this T sqlModel)
    {
        return GetProperties(typeof(T))
            .ToDictionary(
                prop => prop.Name,
                prop =>
                {
                    var propValue = prop.GetValue(sqlModel);

                    if (propValue == null)
                    {
                        return null;
                    }

                    var templateAttribute = prop.GetCustomAttribute<ValueTemplateAttribute>();
                    var template = templateAttribute?.Template ?? defaultValueTemplate;

                    return (object)string.Format(template, propValue);
                });
    }
    
    private static (string schema, string name) GetTableMeta(MemberInfo tableType)
    {
        var attribute = tableType.GetCustomAttribute<TableAttribute>();
        return (attribute?.Schema ?? "dbo", attribute?.Name ?? tableType.Name);
    }

    private static string CreateExpressionFromModel<T>(
        T model, 
        Func<PropertyInfo, bool> include,
        Func<PropertyInfo, string> operatorSelector,
        Func<ICollection<string>, string> conditionLinker,
        char paramPrefix,
        bool skipNull)
    {
        var properties = GetProperties(typeof(T));
        var conditions = new List<string>(properties.Length);

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var property in properties)
        {
            if (!include(property))
            {
                continue;
            }
            
            var propValue = property.GetValue(model);

            if (propValue == null && skipNull)
            {
                continue;
            }

            var columnNameAttribute = property.GetCustomAttribute<ColumnAttribute>();
            var columnName = columnNameAttribute?.Name ?? property.Name;

            conditions.Add($"{columnName} {operatorSelector(property)} {paramPrefix}{property.Name}");
        }

        return conditions.Count == 0 ? string.Empty : conditionLinker(conditions);
    }

    private static PropertyInfo[] GetProperties(Type type, BindingFlags? flags = null)
    {
        flags ??= BindingFlags.Instance | BindingFlags.Public;
        
        if (!TypeProperties.TryGetValue(type, out var properties))
        {
            properties = type.GetProperties(flags.Value);
            TypeProperties.TryAdd(type, properties);
        }

        return properties;
    }

    private static IEnumerable<PropertyInfo> WithoutIgnored(this IEnumerable<PropertyInfo> properties, bool withKeys)
    {
        return properties
            .Where(prop => prop.GetCustomAttribute<IgnoredAttribute>() == null)
            .Where(prop => withKeys || prop.GetCustomAttribute<KeyAttribute>() == null);
    }
}