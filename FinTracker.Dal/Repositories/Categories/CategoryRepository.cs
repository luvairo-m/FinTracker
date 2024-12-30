using FinTracker.Dal.Models.Categories;
using Vostok.Logging.Abstractions;

namespace FinTracker.Dal.Repositories.Categories;

public class CategoryRepository : 
    RepositoryBase<Category, CategorySearch, CategoryUpdate>,
    ICategoryRepository
{
    protected override string EntityName => nameof(Category);
    
    public CategoryRepository(string connectionString, ILog log) 
        : base(connectionString, log?.ForContext<CategoryRepository>())
    {
    }
}