using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using FinTracker.Dal.Logic.Attributes;
using FinTracker.Dal.Logic.Utils;
using ColumnAttribute = FinTracker.Dal.Logic.Attributes.ColumnAttribute;

namespace FinTracker.Dal.Logic.Extensions;

internal static class SqlExtensions
{
    private const char defaultParamPrefix = '@';
    private const string defaultTemplate = "{0}";
    
    private static readonly ConcurrentDictionary<Type, PropertyInfo[]> TypeProperties = new();
    private static readonly ConcurrentDictionary<Type, string> TypeKeyColumns = new();
    
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

    public static string GetKeyColumnName(this Type type)
    {
        return TypeKeyColumns.GetOrAdd(
            type, 
            _ => GetProperties(type)
                .Where(prop => prop.GetCustomAttribute<KeyAttribute>() != null)
                .Select(prop => prop.GetCustomAttribute<ColumnAttribute>()?.Name ?? prop.Name)
                .First());
    }
    
    public static string ToWhereExpression<T>(this T sqlModel, bool skipNull = true)
    {
        var conditions = CreateConditionsFromModel(
            sqlModel,
            include: _ => true,
            operatorSelector: prop =>
            {
                var sqlOperatorAttribute = prop.GetCustomAttribute<SqlOperatorAttribute>();
                var propType = prop.PropertyType;

                return sqlOperatorAttribute == null 
                    ? propType == typeof(string) 
                        ? SqlOperators.Like
                        : propType.IsAssignableTo(typeof(IEnumerable))
                            ? SqlOperators.In
                            : SqlOperators.Equal
                    : sqlOperatorAttribute.Operator;
            },
            paramPrefix: defaultParamPrefix,
            skipNull: skipNull);

        return SqlRequestUtils.BuildWhereExpression(conditions);
    }
    
    public static string ToSetExpression<T>(this T sqlModel, bool skipNull = true)
    {
        var conditions = CreateConditionsFromModel(
            sqlModel,
            include: prop => prop.GetCustomAttribute<ReadOnlyAttribute>() is not { IsReadOnly: true },
            operatorSelector: _ => "=", 
            paramPrefix: defaultParamPrefix,
            skipNull: skipNull);

        return SqlRequestUtils.BuildSetExpression(conditions);
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

                    if (prop.PropertyType == typeof(string))
                    {
                        var templateAttribute = prop.GetCustomAttribute<StringValueTemplateAttribute>();
                        var template = templateAttribute?.Template ?? defaultTemplate;
                        
                        return string.Format(template, propValue);
                    }

                    return propValue;
                });
    }
    
    private static (string schema, string name) GetTableMeta(MemberInfo tableType)
    {
        var attribute = tableType.GetCustomAttribute<TableAttribute>();
        return (attribute?.Schema ?? "dbo", attribute?.Name ?? tableType.Name);
    }

    private static ICollection<string> CreateConditionsFromModel<T>(
        T model, 
        Func<PropertyInfo, bool> include,
        Func<PropertyInfo, string> operatorSelector,
        char paramPrefix,
        bool skipNull)
    {
        return GetProperties(typeof(T))
            .Where(prop => prop.GetCustomAttribute<IgnoredAttribute>() == null)
            .Where(prop => include(prop) && (!skipNull || prop.GetValue(model) != null))
            .Select(prop =>
            {
                var columnNameAttribute = prop.GetCustomAttribute<ColumnAttribute>();
                var columnName = columnNameAttribute?.Name ?? prop.Name;
                var columnTemplate = columnNameAttribute?.Template ?? defaultTemplate;
                
                return $"{string.Format(columnTemplate, columnName)} {operatorSelector(prop)} {paramPrefix}{prop.Name}";
            })
            .ToArray();
    }

    private static PropertyInfo[] GetProperties(Type type, BindingFlags? flags = null)
    {
        return TypeProperties.GetOrAdd(type, _ => type.GetProperties(flags ?? BindingFlags.Instance | BindingFlags.Public));
    }

    private static IEnumerable<PropertyInfo> WithoutIgnored(this IEnumerable<PropertyInfo> properties, bool withKeys)
    {
        return properties
            .Where(prop => prop.GetCustomAttribute<IgnoredAttribute>() == null)
            .Where(prop => withKeys || prop.GetCustomAttribute<KeyAttribute>() == null);
    }
}