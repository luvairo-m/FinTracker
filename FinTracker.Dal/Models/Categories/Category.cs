using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinTracker.Dal.Models.Abstractions;

namespace FinTracker.Dal.Models.Categories;

/// <summary>
/// Категория платежа.
/// </summary>
[Table("Category", Schema = "dbo")]
public class Category : IEntity
{
    /// <summary>
    /// Идентификатор категории.
    /// </summary>
    [Key]
    [Column("Id")]
    [ReadOnly(isReadOnly: true)]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название категории.
    /// </summary>
    [Column("Title")]
    public string Title { get; set; }
    
    /// <summary>
    /// Описание категории (опционально).
    /// </summary>
    [Column("Description")]
    public string Description { get; set; }
}