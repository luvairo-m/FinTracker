namespace FinTracker.Logic.Models.Category.Params;

public struct UpdateCategoryParam
{
    public Guid CategoryId { get; set; }
    
    public string Title { get; set; }
}