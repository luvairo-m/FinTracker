using FinTracker.Dal.Logic.Connections;
using FinTracker.Dal.Models.Categories;
using FinTracker.Dal.Repositories.Categories;
using NUnit.Framework;
using Vostok.Logging.Abstractions;

namespace FinTracker.Integration.Tests.Repositories;

[TestFixture]
public class CategoryRepositoryTests : RepositoryBaseTests<Category, CategorySearch>
{
    public CategoryRepositoryTests()
    {
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

    protected override CategorySearch CreateSearchModel(Category model, bool byIdOnly = false)
    {
        var search = new CategorySearch { Id = model.Id };
        
        if (!byIdOnly)
        {
            search.TitleSubstring = model.Title;
        }

        return search;
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