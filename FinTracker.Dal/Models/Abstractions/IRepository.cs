using FinTracker.Dal.Logic;

namespace FinTracker.Dal.Models.Abstractions;

public interface IRepository<TModel, in TSearchModel>
    where TModel : IEntity
{
    Task<DbQueryResult<Guid>> AddAsync(TModel model, TimeSpan? timeout = null);

    Task<DbQueryResult<ICollection<TModel>>> SearchAsync(
        TSearchModel search, 
        int skip = 0, 
        int take = int.MaxValue,
        TimeSpan? timeout = null);

    Task<DbQueryResult> UpdateAsync(TModel update, TimeSpan? timeout = null);

    Task<DbQueryResult> RemoveAsync(Guid id, TimeSpan? timeout = null);
}