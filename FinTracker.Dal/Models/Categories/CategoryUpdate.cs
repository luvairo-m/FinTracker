namespace FinTracker.Dal.Models.Categories;

/// <summary>
/// Модель обновления категории.
/// </summary>
public class CategoryUpdate
{
    public string Title { get; set; }
    
    public string Description { get; set; }
}