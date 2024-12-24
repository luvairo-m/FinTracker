using System.Collections;
using System.Collections.Concurrent;
using System.Reflection;
using FinTracker.Dal.Logic.Attributes;

namespace FinTracker.Dal.Logic.Utils;

public static class SqlRequestUtils
{
    private static readonly ConcurrentDictionary<Type, PropertyInfo[]> TypeProperties = new();

    public static IEnumerable<string> GetColumnNames(
        this Type type, 
        bool includeKeys = true, 
        Func<string, string> replacer = null)
    {
        if (!TypeProperties.TryGetValue(type, out var properties))
        {
            properties = type.GetProperties();
            TypeProperties.TryAdd(type, properties);
        }

        foreach (var propertyInfo in properties)
        {
            var columnAttribute = propertyInfo.GetCustomAttribute<ColumnAttribute>();

            if (columnAttribute is null)
            {
                continue;
            }
                
            if (columnAttribute.IsIgnored)
            {
                continue;
            }

            if (columnAttribute.IsKey && !includeKeys)
            {
                continue;
            }
                
            yield return replacer is null ? columnAttribute.Name : replacer(columnAttribute.Name);
        }
    }
        
    public static string GetColumnNamesString(
        this Type type, 
        char separator = ',', 
        bool includeKeys = true, 
        Func<string, string> replacer = null)
    {
        return string.Join(separator, GetColumnNames(type, includeKeys, replacer));
    }

    public static string BuildWhereExpression(ICollection<string> whereCollector)
    {
        return whereCollector.Count == 0 ? string.Empty : $"WHERE {string.Join(" AND ", whereCollector)}";
    }

    public static string ToFilterDescription<T>(this T filterModel)
    {
        var modelType = typeof(T);
        
        var properties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var conditions = new List<string>(properties.Length);

        foreach (var property in properties)
        {
            var propValue = property.GetValue(filterModel);

            if (propValue == null)
            {
                continue;
            }

            var displayValue = property.PropertyType != typeof(string) && propValue is IEnumerable enumerable
                ? $"[ {string.Join("; ", enumerable)} ]"
                : propValue.ToString();
            
            conditions.Add($"{property.Name} = {displayValue}");
        }

        return conditions.Count == 0 ? "( Empty )" : $"( {string.Join(", ", conditions)} )";
    }
}