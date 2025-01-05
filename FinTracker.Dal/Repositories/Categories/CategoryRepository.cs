using FinTracker.Dal.Logic.Connections;
using FinTracker.Dal.Models.Categories;
using Vostok.Logging.Abstractions;

namespace FinTracker.Dal.Repositories.Categories;

public class CategoryRepository : RepositoryBase<Category, CategorySearch>, ICategoryRepository
{
    protected override string EntityName => nameof(Category);
    
    public CategoryRepository(ISqlConnectionFactory connectionFactory, ILog log) 
        : base(connectionFactory, log?.ForContext<CategoryRepository>())
    {
    }
}