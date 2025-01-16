using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinTracker.Dal.Models.Abstractions;

namespace FinTracker.Dal.Models.Accounts;

/// <summary>
/// Счет.
/// </summary>
[Table("Account", Schema = "dbo")]
public class Account : IEntity
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
    /// Описание счета (опционально).
    /// </summary>
    [Column("Description")]
    public string Description { get; set; }
    
    /// <summary>
    /// Идентификатор валюты счета.
    /// </summary>
    [Column("CurrencyId")]
    public Guid? CurrencyId { get; set; }
}