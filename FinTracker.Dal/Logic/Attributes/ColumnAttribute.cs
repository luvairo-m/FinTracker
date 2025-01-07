namespace FinTracker.Dal.Logic.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ColumnAttribute : Attribute
{
    public string Name { get; }
    public string Template { get; init; }
    
    public ColumnAttribute(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        this.Name = name;
    }
}