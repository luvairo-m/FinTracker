using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    [Column("Id")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название платежа.
    /// </summary>
    [Column("Title")]
    public string Title { get; set; }
    
    /// <summary>
    /// Описание платежа.
    /// </summary>
    [Column("Description")]
    public string Description { get; set; }
    
    /// <summary>
    /// Сумма платежа.
    /// </summary>
    [Column("Amount")]
    [ReadOnly(isReadOnly: true)]
    public decimal? Amount { get; set; }
    
    /// <summary>
    /// Тип операции.
    /// Пример: доход, расход.
    /// </summary>
    [Column("Type")]
    [ReadOnly(isReadOnly: true)]
    public OperationType? Type { get; set; }
    
    /// <summary>
    /// Дата совершения платежа (UTC).
    /// </summary>
    [Column("Date")]
    [ReadOnly(isReadOnly: true)]
    public DateTime? Date { get; set; }
    
    /// <summary>
    /// Идентификатор счета платежа.
    /// </summary>
    [Column("BillId")]
    [ReadOnly(isReadOnly: true)]
    public Guid? BillId { get; set; }
    
    /// <summary>
    /// Идентификатор валюты платежа.
    /// </summary>
    [Column("CurrencyId")]
    [ReadOnly(isReadOnly: true)]
    public Guid? CurrencyId { get; set; }
    
    /// <summary>
    /// Идентификатор категории платежа.
    /// </summary>
    [Column("CategoryId")]
    public Guid? CategoryId { get; set; }
}