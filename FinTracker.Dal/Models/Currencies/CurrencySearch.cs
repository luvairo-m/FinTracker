using System.ComponentModel.DataAnnotations.Schema;

namespace FinTracker.Dal.Models.Currencies;

public class CurrencySearch
{
    public Guid? Id { get; set; }
    
    [Column("Title")]
    public string TitleSubstring { get; set; }
}