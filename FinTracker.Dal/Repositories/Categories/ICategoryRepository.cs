using FinTracker.Dal.Logic;
using FinTracker.Dal.Models.Categories;

namespace FinTracker.Dal.Repositories.Categories;

/// <summary>
/// Репозиторий для работы с платежными категориями.
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    /// Добавить новую категорию.
    /// </summary>
    /// <param name="category"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    Task<DbQueryResult<Guid>> AddAsync(Category category, TimeSpan? timeout = null);

    /// <summary>
    /// Поиск по категориям.
    /// </summary>
    /// <param name="search"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    Task<DbQueryResult<ICollection<Category>>> SearchAsync(CategorySearch search, TimeSpan? timeout = null);

    /// <summary>
    /// Удалить категорию.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    Task<DbQueryResult> RemoveAsync(Guid id, TimeSpan? timeout = null);

    /// <summary>
    /// Обновить категорию по идентификатору.
    /// </summary>
    /// <param name="update"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    Task<DbQueryResult> UpdateAsync(Category update, TimeSpan? timeout = null);
}