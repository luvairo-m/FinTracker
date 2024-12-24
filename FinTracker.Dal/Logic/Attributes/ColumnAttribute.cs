namespace FinTracker.Dal.Logic.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ColumnAttribute : Attribute
{
    public string Name { get; set; }
    
    public bool IsKey { get; set; }
    
    public bool IsIgnored { get; set; }
}