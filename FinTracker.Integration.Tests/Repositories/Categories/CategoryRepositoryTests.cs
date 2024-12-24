using FinTracker.Dal.Logic;
using FinTracker.Dal.Models.Categories;
using FinTracker.Dal.Repositories.Categories;
using FinTracker.Integration.Tests.Utils;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Logging.Abstractions;

namespace FinTracker.Integration.Tests.Repositories.Categories;

public class CategoryRepositoryTests
{
    private readonly DatabaseInitializer databaseInitializer = new(
        TestCredentials.FinTrackerConnectionString,
        Directory.GetFiles("Scripts/Categories/Create", "*.sql", SearchOption.AllDirectories),
        Directory.GetFiles("scripts/Categories/Remove", "*.sql", SearchOption.AllDirectories));
    
    private readonly ICategoryRepository categoryRepository = new CategoryRepository(
        TestCredentials.FinTrackerConnectionString, 
        new SilentLog());

    private readonly Category[] categories = 
    {
        new() { Title = "Спорт" },
        new() { Title = "Еда" },
        new() { Title = "Обучение" }
    };

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await this.databaseInitializer.DropTablesAsync();
        (await this.databaseInitializer.CreateTablesAsync()).EnsureSuccess();
        
        foreach (var category in categories)
        {
            var addResult = await this.categoryRepository.AddAsync(category);
            addResult.EnsureSuccess();

            category.Id = addResult.Result;
        }
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await this.databaseInitializer.DropTablesAsync();
    }

    [Test(Description = "Category title must be unique across the table.")]
    public async Task AddAsync_Duplicate()
    {
        // Arrange
        var category = categories.First();
        
        // Act
        var addResult = await this.categoryRepository.AddAsync(category);
        
        // Assert
        addResult.Status.Should().Be(DbQueryResultStatus.Error);
    }

    [Test]
    public async Task SearchAsync_Success()
    {
        // Arrange
        var category = this.categories.Skip(1).First();
        var searchResults = new List<DbQueryResult<ICollection<Category>>>();
        
        // Act
        foreach (var filter in GetSuccessfulFilters(category))
        {
            searchResults.Add(await this.categoryRepository.SearchAsync(filter));
        }
        
        // Assert
        foreach (var result in searchResults)
        {
            result.Status.Should().Be(DbQueryResultStatus.Ok);
            result.Result.Should().HaveCount(1).And.ContainSingle(cat => cat.Id == category.Id && cat.Title == category.Title);
        }
    }

    [Test]
    public async Task SearchAsync_NotFound()
    {
        // Arrange
        var filter = new CategoryFilter { Id = Guid.Empty };
        
        // Act
        var searchResult = await this.categoryRepository.SearchAsync(filter);
        
        // Assert
        searchResult.Status.Should().Be(DbQueryResultStatus.NotFound);
    }

    [Test]
    public async Task RemoveAsync_Success()
    {
        // Arrange
        var categoryId = this.categories.First().Id;
        
        // Act
        var removeResult = await this.categoryRepository.RemoveAsync(categoryId);
        
        // Assert
        removeResult.Status.Should().Be(DbQueryResultStatus.Ok);
        
        var searchResult = await this.categoryRepository.SearchAsync(new CategoryFilter { Id = categoryId });
        searchResult.Status.Should().Be(DbQueryResultStatus.NotFound);
    }

    [Test]
    public async Task UpdateAsync_Success()
    {
        // Arrange
        const string newTitle = "Учеба";
        
        var filter = new CategoryFilter { Id = this.categories.Last().Id };
        
        // Act
        var updateResult = await this.categoryRepository.UpdateAsync(filter, newTitle);
        
        // Assert
        updateResult.Status.Should().Be(DbQueryResultStatus.Ok);
        
        var searchResult = await this.categoryRepository.SearchAsync(filter);
        searchResult.Status.Should().Be(DbQueryResultStatus.Ok);
        searchResult.Result.Should().HaveCount(1).And.ContainSingle(cat => cat.Title == newTitle);
    }

    private static IEnumerable<CategoryFilter> GetSuccessfulFilters(Category category)
    {
        yield return new CategoryFilter
        {
            Id = category.Id,
            SearchText = category.Title
        };
        
        yield return new CategoryFilter
        {
            Id = category.Id
        };
        
        yield return new CategoryFilter
        {
            SearchText = category.Title
        };
    }
}