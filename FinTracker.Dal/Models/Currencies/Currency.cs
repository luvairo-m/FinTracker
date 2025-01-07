using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinTracker.Dal.Models.Abstractions;

namespace FinTracker.Dal.Models.Currencies;

/// <summary>
/// Валюта.
/// </summary>
[Table("Currency", Schema = "dbo")]
public class Currency : IEntity
{
    /// <summary>
    /// Идентификатор валюты.
    /// </summary>
    [Key]
    [Column("Id")]
    [ReadOnly(isReadOnly: true)]
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование валюты.
    /// </summary>
    [Column("Title")]
    public string Title { get; set; }
    
    /// <summary>
    /// Знак валюты.
    /// </summary>
    [Column("Sign")]
    public string Sign { get; set; }
}