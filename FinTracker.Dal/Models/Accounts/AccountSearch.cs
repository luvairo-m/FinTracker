using FinTracker.Dal.Logic.Attributes;

namespace FinTracker.Dal.Models.Accounts;

public class AccountSearch
{
    [Column("Id")]
    public Guid? Id { get; set; }
    
    [Column("Title")]
    [StringValueTemplate("%{0}%")]
    public string TitleSubstring { get; set; }
    
    [Column("CurrencyId")]
    public Guid? CurrencyId { get; set; }

    public static AccountSearch ById(Guid id)
    {
        return new AccountSearch { Id = id };
    }
}