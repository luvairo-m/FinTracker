namespace FinTracker.Dal.Logic.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class StringValueTemplateAttribute : Attribute
{
    public string Template { get; }

    public StringValueTemplateAttribute(string template)
    {
        ArgumentNullException.ThrowIfNull(template);
        this.Template = template;
    }
}