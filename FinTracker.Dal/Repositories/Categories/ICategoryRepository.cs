using FinTracker.Dal.Models.Abstractions;
using FinTracker.Dal.Models.Categories;

namespace FinTracker.Dal.Repositories.Categories;

/// <summary>
/// Репозиторий для работы с платежными категориями.
/// </summary>
public interface ICategoryRepository : IRepository<Category, CategorySearch>
{
}