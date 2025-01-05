namespace FinTracker.Dal.Logic.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ValueTemplateAttribute : Attribute
{
    public string Template { get; }

    public ValueTemplateAttribute(string template)
    {
        this.Template = template;
    }
}