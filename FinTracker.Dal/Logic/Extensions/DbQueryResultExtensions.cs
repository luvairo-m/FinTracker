namespace FinTracker.Dal.Logic.Extensions;

public static class DbQueryResultExtensions
{
    public static T FirstOrDefault<T>(this DbQueryResult<ICollection<T>> collectionResult)
    {
        collectionResult.EnsureSuccess();
        
        return collectionResult.Result.FirstOrDefault();
    }
}