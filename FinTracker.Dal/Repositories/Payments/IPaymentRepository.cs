using FinTracker.Dal.Logic;
using FinTracker.Dal.Models.Abstractions;
using FinTracker.Dal.Models.Payments;

namespace FinTracker.Dal.Repositories.Payments;

/// <summary>
/// Репозиторий для работы с платежами.
/// </summary>
public interface IPaymentRepository : IRepository<Payment, PaymentSearch>
{
    Task<DbQueryResult> UpdateCategoriesAsync(
        Guid paymentId,
        ICollection<Guid> addCategories,
        ICollection<Guid> removeCategories,
        TimeSpan? timeout = null);
}