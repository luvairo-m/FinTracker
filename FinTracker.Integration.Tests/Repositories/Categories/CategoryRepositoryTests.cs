using FinTracker.Dal.Models.Categories;
using FinTracker.Dal.Repositories.Categories;
using FinTracker.Integration.Tests.Utils;
using Vostok.Logging.Abstractions;

namespace FinTracker.Integration.Tests.Repositories.Categories;

public class CategoryRepositoryTests : RepositoryBaseTests<Category, CategorySearch, CategoryUpdate>
{
    public CategoryRepositoryTests()
    {
        this.databaseInitializer = new DatabaseInitializer(
            TestCredentials.FinTrackerConnectionString,
            Directory.GetFiles("Scripts/Categories/Create", "*.sql", SearchOption.AllDirectories),
            Directory.GetFiles("Scripts/Categories/Drop", "*.sql", SearchOption.AllDirectories));

        this.repository = new CategoryRepository(TestCredentials.FinTrackerConnectionString, new SilentLog());
    }

    protected override async Task<ICollection<Category>> CreateModelsInRepository(int count)
    {
        var categories = Enumerable
            .Range(0, count)
            .Select(_ => new Category 
            { 
                Title = Guid.NewGuid().ToString(), 
                Description = Guid.NewGuid().ToString() 
            })
            .ToList();

        foreach (var category in categories)
        {
            var addResult = await this.repository.AddAsync(category);
            addResult.EnsureSuccess();

            category.Id = addResult.Result;
            this.addedIds.Add(addResult.Result);
        }

        return categories;
    }

    protected override IEnumerable<CategorySearch> CreateSearchModels(Category model, bool byIdOnly = false)
    {
        if (model == null)
        {
            yield return new CategorySearch();
            yield break;
        }

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

    protected override (CategoryUpdate update, Category updated) CreateMetaForUpdate(Category model)
    {
        var update = new CategoryUpdate
        {
            Title = model.Title + model.Title,
            Description = model.Description + model.Description
        };

        var updated = new Category
        {
            Id = model.Id,
            Title = update.Title,
            Description = update.Description
        };

        return (update, updated);
    }

    protected override bool Equals(Category first, Category second)
    {
        return first.Id == second.Id && first.Title == second.Title && first.Description == second.Description;
    }
}