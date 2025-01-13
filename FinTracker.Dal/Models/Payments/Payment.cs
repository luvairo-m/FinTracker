using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinTracker.Dal.Logic.Attributes;
using FinTracker.Dal.Models.Abstractions;

namespace FinTracker.Dal.Models.Payments;

/// <summary>
/// Платеж.
/// </summary>
[Table("Payment", Schema = "dbo")]
public class Payment : IEntity
{
    /// <summary>
    /// Идентификатор платежа.
    /// </summary>
    [Key]
    [ReadOnly(isReadOnly: true)]
    [Logic.Attributes.Column("Id")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название платежа.
    /// </summary>
    [Logic.Attributes.Column("Title")]
    public string Title { get; set; }
    
    /// <summary>
    /// Описание платежа (опционально).
    /// </summary>
    [Logic.Attributes.Column("Description")]
    public string Description { get; set; }
    
    /// <summary>
    /// Сумма платежа.
    /// </summary>
    [Logic.Attributes.Column("Amount")]
    public decimal? Amount { get; set; }
    
    /// <summary>
    /// Тип операции.
    /// Пример: доход, расход.
    /// </summary>
    [Logic.Attributes.Column("Type")]
    public OperationType? Type { get; set; }
    
    /// <summary>
    /// Дата совершения платежа (UTC).
    /// </summary>
    [Logic.Attributes.Column("Date")]
    public DateTime? Date { get; set; }
    
    /// <summary>
    /// Идентификатор счета платежа.
    /// </summary>
    [Logic.Attributes.Column("BillId")]
    public Guid? BillId { get; set; }
    
    /// <summary>
    /// Категории платежа.
    /// </summary>
    [Ignored]
    public ICollection<Guid> Categories { get; set; } = new List<Guid>();
}