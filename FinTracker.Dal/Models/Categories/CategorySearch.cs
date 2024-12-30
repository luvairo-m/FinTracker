using System.ComponentModel.DataAnnotations.Schema;

namespace FinTracker.Dal.Models.Categories;

/// <summary>
/// Модель поиска категории.
/// </summary>
public class CategorySearch
{
    public Guid? Id { get; set; }
    
    [Column("Title")]
    public string TitleSubstring { get; set; }
}