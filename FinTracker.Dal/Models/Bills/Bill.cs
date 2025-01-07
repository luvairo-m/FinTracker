using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinTracker.Dal.Models.Abstractions;

namespace FinTracker.Dal.Models.Bills;

/// <summary>
/// Счет.
/// </summary>
[Table("Bill", Schema = "dbo")]
public class Bill : IEntity
{
    /// <summary>
    /// Идентификатор счета.
    /// </summary>
    [Key]
    [Column("Id")]
    [ReadOnly(isReadOnly: true)]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Баланс счета.
    /// </summary>
    [Column("Balance")]
    public decimal? Balance { get; set; }
    
    /// <summary>
    /// Название счета.
    /// </summary>
    [Column("Title")]
    public string Title { get; set; }

    /// <summary>
    /// Описание счета.
    /// </summary>
    [Column("Description")]
    public string Description { get; set; }
    
    /// <summary>
    /// Идентификатор валюты счета.
    /// </summary>
    [Column("CurrencyId")]
    public Guid? CurrencyId { get; set; }
}