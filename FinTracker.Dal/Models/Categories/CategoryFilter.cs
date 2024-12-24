using FinTracker.Dal.Logic.Utils;

namespace FinTracker.Dal.Models.Categories;

public class CategoryFilter
{
    public Guid? Id { get; set; }
    
    public string SearchText { get; set; }

    public string ToWhereExpression()
    {
        var whereCollector = new List<string>(2);

        if (this.Id != null)
        {
            whereCollector.Add($"Id = @{nameof(this.Id)}");
        }

        if (this.SearchText != null)
        {
            whereCollector.Add($"Title LIKE @{nameof(this.SearchText)}");
        }

        return SqlRequestUtils.BuildWhereExpression(whereCollector);
    }

    public Dictionary<string, object> ToParametersWithValues()
    {
        return new Dictionary<string, object>
        {
            [nameof(this.Id)] = this.Id,
            [nameof(this.SearchText)] = $"%{this.SearchText}%"
        };
    }
}