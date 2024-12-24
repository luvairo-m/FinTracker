using FinTracker.Dal.Logic.Attributes;

namespace FinTracker.Dal.Models.Categories;

/// <summary>
/// Категория платежа.
/// </summary>
[Table(Name = "Category", Schema = "dbo")]
public class Category
{
    /// <summary>
    /// Идентификатор категории.
    /// </summary>
    [Column(Name = "Id")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название категории.
    /// </summary>
    [Column(Name = "Title")]
    public string Title { get; set; }
}