using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinTracker.Dal.Models.Bills;

/// <summary>
/// Счет.
/// </summary>
public class Bill : IEntity
{
    /// <summary>
    /// Идентификатор счета.
    /// </summary>
    [Key]
    [Column("Id")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Баланс счета.
    /// </summary>
    [Column("Balance")]
    public double Balance { get; set; }
    
    /// <summary>
    /// Название счета.
    /// </summary>
    [Column("Title")]
    public string Title { get; set; }

    /// <summary>
    /// Описание счета.
    /// </summary>
    [Column("Description")]
    private string Description { get; set; }
}