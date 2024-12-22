namespace FinTracker.Logic.Models.Category.Results;

public struct GetCategoriesResult
{
    public ICollection<GetCategoryResult> Categories { get; set; }
}