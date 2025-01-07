namespace FinTracker.Dal.Models.Payments;

/// <summary>
/// Тип финансовой операции.
/// </summary>
public enum OperationType
{
    /// <summary>
    /// Доход.
    /// </summary>
    Income,
    
    /// <summary>
    /// Расход.
    /// </summary>
    Outcome
}