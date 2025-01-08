using FinTracker.Infra.Extensions;

namespace FinTracker.Dal.Logic.Utils;

internal static class SqlRequestUtils
{
    public static string BuildWhereExpression(ICollection<string> whereCollector, string @operator = "AND")
    {
        return whereCollector.Count == 0 ? string.Empty : $"WHERE {string.Join($" {@operator} ", whereCollector)}";
    }
    
    public static string BuildSetExpression(ICollection<string> setCollector)
    {
        return setCollector.Count == 0 ? string.Empty : $"SET {setCollector.AsCommaSeparated()}";
    }
}