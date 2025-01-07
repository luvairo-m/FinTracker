namespace FinTracker.Logic.Models.Category;

public struct GetCategoryModel
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }

    public string Description { get; set; }
}