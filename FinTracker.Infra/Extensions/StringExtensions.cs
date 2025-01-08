namespace FinTracker.Infra.Extensions;

public static class StringExtensions
{
    public static string AsCommaSeparated(this IEnumerable<string> lines)
    {
        return string.Join(", ", lines);
    }
}