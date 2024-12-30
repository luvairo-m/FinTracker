using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование валюты.
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Знак валюты.
    /// </summary>
    public string Sign { get; set; }
}