namespace FinTracker.Dal.Logic.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class SqlOperatorAttribute : Attribute
{
    public string Operator { get; }

    public SqlOperatorAttribute(string sqlOperator)
    {
        ArgumentNullException.ThrowIfNull(nameof(sqlOperator));
        this.Operator = sqlOperator;
    }
}