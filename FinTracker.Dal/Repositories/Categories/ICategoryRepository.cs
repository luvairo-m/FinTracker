using FinTracker.Dal.Logic;
using FinTracker.Dal.Models.Categories;

namespace FinTracker.Dal.Repositories.Categories;

/// <summary>
/// Репозиторий для работы с категориями.
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    /// Добавить категорию.
    /// </summary>
    /// <param name="category"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    Task<DbQueryResult<Guid>> AddAsync(Category category, TimeSpan? timeout = null);

    /// <summary>
    /// Удалить категорию по идентификатору.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    Task<DbQueryResult> RemoveAsync(Guid id, TimeSpan? timeout = null);
    
    /// <summary>
    /// Найти категорию по фильтру.
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    Task<DbQueryResult<ICollection<Category>>> SearchAsync(CategoryFilter filter, TimeSpan? timeout = null);

    /// <summary>
    /// Обновить название категории.
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="newTitle"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    Task<DbQueryResult> UpdateAsync(CategoryFilter filter, string newTitle, TimeSpan? timeout = null);
}