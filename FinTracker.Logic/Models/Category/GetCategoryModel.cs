namespace FinTracker.Logic.Models.Category;

public struct GetCategoryModel
{
    public Guid CategoryId { get; set; }
    
    public string Title { get; set; }
}