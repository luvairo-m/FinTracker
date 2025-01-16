using FinTracker.Dal.Logic;
using FinTracker.Dal.Logic.Attributes;

namespace FinTracker.Dal.Models.Payments;

public class PaymentSearch
{
    [Column("Id")]
    public Guid? Id { get; set; }
    
    [Column("Title")]
    [StringValueTemplate("%{0}%")]
    public string TitleSubstring { get; set; }
    
    /// <summary>
    /// Минимальное значение включается.
    /// </summary>
    [Column("Amount")]
    [SqlOperator(SqlOperators.GreaterOrEqual)]
    public decimal? MinAmount { get; set; }
    
    /// <summary>
    /// Максимальное значение включается.
    /// </summary>
    [Column("Amount")]
    [SqlOperator(SqlOperators.LessOrEqual)]
    public decimal? MaxAmount { get; set; }
    
    [Column("Type")]
    public OperationType[] Types { get; set; }
    
    /// <summary>
    /// Минимальное значение включается.
    /// </summary>
    [Column("Date")]
    [SqlOperator(SqlOperators.GreaterOrEqual)]
    public DateTime? MinDate { get; set; }
    
    /// <summary>
    /// Максимальное значение включается.
    /// </summary>
    [Column("Date")]
    [SqlOperator(SqlOperators.LessOrEqual)]
    public DateTime? MaxDate { get; set; }
    
    [Column("Date", Template = "MONTH({0})")]
    public int[] Months { get; set; }
    
    [Column("Date", Template = "YEAR({0})")]
    public int[] Years { get; set; }
    
    [Column("AccountId")]
    public Guid? AccountId { get; set; }
    
    [Ignored]
    public ICollection<Guid> Categories { get; set; }
}