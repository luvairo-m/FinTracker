using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using FinTracker.Dal.Logic.Attributes;
using FinTracker.Dal.Logic.Utils;

namespace FinTracker.Dal.Logic.Extensions;

internal static class SqlExtensions
{
    private static readonly ConcurrentDictionary<Type, PropertyInfo[]> TypeProperties = new();
    
    public static string GetTableName(this Type tableType)
    {
        var tableMeta = GetTableMeta(tableType);
        return $"{tableMeta.schema}.{tableMeta.name}";
    }
    
    public static IEnumerable<string> GetColumnNames(this Type type, bool includeKeys = true)
    {
        return GetProperties(type)
            .FilterSqlProperties(includeKeys: includeKeys)
            .Select(prop => prop.GetCustomAttribute<ColumnAttribute>()?.Name ?? prop.Name);
    }

    public static IEnumerable<string> GetParameterNames(this Type type, char paramPrefix = '@', bool includeKeys = true)
    {
        return GetProperties(type)
            .FilterSqlProperties(includeKeys: includeKeys)
            .Select(prop => $"{paramPrefix}{prop.Name}");
    }
    
    public static string ToWhereExpression<T>(this T sqlModel, bool skipNull = true)
    {
        return CreateExpressionFromModel(
            sqlModel,
            operatorSelector: prop => prop.PropertyType == typeof(string)
                ? "LIKE"
                : prop.PropertyType.IsAssignableTo(typeof(IEnumerable))
                    ? "IN"
                    : "=",
            conditionLinker: conditions => SqlRequestUtils.BuildWhereExpression(conditions),
            paramPrefix: '@',
            skipNull: skipNull);
    }
    
    public static string ToSetExpression<T>(this T sqlModel, bool skipNull = true)
    {
        return CreateExpressionFromModel(
            sqlModel,
            operatorSelector: _ => "=", 
            conditionLinker: SqlRequestUtils.BuildSetExpression, 
            paramPrefix: '@',
            skipNull: skipNull);
    }

    public static Dictionary<string, object> ToParametersWithValues<T>(
        this T sqlModel, 
        Func<Type, object, object> resultSelector = null)
    {
        return GetProperties(typeof(T))
            .ToDictionary(
                prop => prop.Name,
                prop =>
                {
                    var propValue = prop.GetValue(sqlModel);
                    return resultSelector == null ? propValue : resultSelector(prop.PropertyType, propValue);
                });
    }
    
    private static (string schema, string name) GetTableMeta(MemberInfo tableType)
    {
        var attribute = tableType.GetCustomAttribute<TableAttribute>();
        return (attribute?.Schema ?? "dbo", attribute?.Name ?? tableType.Name);
    }

    private static string CreateExpressionFromModel<T>(
        T model, 
        Func<PropertyInfo, string> operatorSelector,
        Func<ICollection<string>, string> conditionLinker,
        char paramPrefix,
        bool skipNull)
    {
        var properties = GetProperties(typeof(T));
        var modelMeta = new List<string>(properties.Length);

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var property in properties)
        {
            var propValue = property.GetValue(model);

            if (propValue == null && skipNull)
            {
                continue;
            }

            var columnNameAttribute = property.GetCustomAttribute<ColumnAttribute>();
            var columnName = columnNameAttribute?.Name ?? property.Name;

            modelMeta.Add($"{columnName} {operatorSelector(property)} {paramPrefix}{property.Name}");
        }

        return conditionLinker(modelMeta);
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

    private static IEnumerable<PropertyInfo> FilterSqlProperties(
        this IEnumerable<PropertyInfo> properties,
        bool includeKeys = true)
    {
        return properties
            .Where(prop => prop.GetCustomAttribute<IgnoredAttribute>() == null)
            .Where(prop => includeKeys || prop.GetCustomAttribute<KeyAttribute>() == null);
    }
}