using FinTracker.Dal.Logic.Connections;
using FinTracker.Dal.Models.Categories;
using FinTracker.Dal.Repositories.Categories;
using FinTracker.Integration.Tests.Utils;
using NUnit.Framework;
using Vostok.Logging.Abstractions;

namespace FinTracker.Integration.Tests.Repositories.Categories;

[TestFixture]
public class CategoryRepositoryTests : RepositoryBaseTests<Category, CategorySearch>
{
    public CategoryRepositoryTests()
    {
        this.databaseInitializer = new DatabaseInitializer(
            TestCredentials.FinTrackerConnectionString,
            Directory.GetFiles("Scripts/Categories/Create", "*.sql", SearchOption.AllDirectories),
            Directory.GetFiles("Scripts/Categories/Drop", "*.sql", SearchOption.AllDirectories));

        this.repository = new CategoryRepository(
            new SqlConnectionFactory(TestCredentials.FinTrackerConnectionString),
            new SilentLog());
    }

    protected override Category CreateModel()
    {
        return new Category
        {
            Title = Guid.NewGuid().ToString(),
            Description = Guid.NewGuid().ToString()
        };
    }

    protected override IEnumerable<CategorySearch> CreateSearchModels(Category model, bool byIdOnly = false)
    {
        if (byIdOnly)
        {
            yield return new CategorySearch { Id = model.Id };
            yield break;
        }
        
        yield return new CategorySearch
        {
            Id = model.Id,
            TitleSubstring = model.Title
        };
        
        yield return new CategorySearch
        {
            Id = model.Id
        };
        
        yield return new CategorySearch
        {
            TitleSubstring = model.Title
        };
    }

    protected override Category ApplyUpdate(Category model, Category update)
    {
        return new Category
        {
            Id = model.Id,
            Title = update.Title ?? model.Title,
            Description = update.Description ?? model.Description
        };
    }
}