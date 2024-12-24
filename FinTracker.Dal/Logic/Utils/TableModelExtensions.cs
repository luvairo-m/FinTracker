using System.Reflection;
using FinTracker.Dal.Logic.Attributes;

namespace FinTracker.Dal.Logic.Utils;

public static class TableModelExtensions
{
    public static string GetTableName(this Type tableModel)
    {
        var tableMeta = GetTableMeta(tableModel);
        return $"{tableMeta.schema}.{tableMeta.name}";
    }
        
    private static (string schema, string name) GetTableMeta(MemberInfo tableModel)
    {
        var attribute = tableModel.GetCustomAttribute<TableAttribute>();
        return (attribute?.Schema ?? "dbo", attribute?.Name ?? tableModel.Name);
    }
}