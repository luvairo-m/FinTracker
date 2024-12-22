namespace FinTracker.Logic.Models.Category.Results;

public struct GetCategoryResult
{
    public Guid CategoryId { get; set; }
    
    public string Title { get; set; }
}