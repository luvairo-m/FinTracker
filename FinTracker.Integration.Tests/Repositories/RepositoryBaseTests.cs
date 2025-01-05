using FinTracker.Dal.Logic;
using FinTracker.Dal.Models.Abstractions;
using FinTracker.Dal.Repositories;
using FinTracker.Integration.Tests.Utils;
using FluentAssertions;
using NUnit.Framework;

namespace FinTracker.Integration.Tests.Repositories;

public abstract class RepositoryBaseTests<TModel, TSearchModel>
    where TModel : IEntity
    where TSearchModel : new()
{
    protected DatabaseInitializer databaseInitializer;
    protected RepositoryBase<TModel, TSearchModel> repository;

    protected readonly List<Guid> currentEntities = new();
    
    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await this.databaseInitializer.DropTablesAsync();
        (await this.databaseInitializer.CreateTablesAsync()).EnsureSuccess();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await this.databaseInitializer.DropTablesAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        foreach (var entityId in this.currentEntities)
        {
            var removeResult = await this.repository.RemoveAsync(entityId);
            removeResult.EnsureSuccess();
        }
    }

    [Test]
    public async Task SearchAsync_Success()
    {
        // Arrange
        var models = await this.CreateModelsInRepository(10);
        var model = models.Skip(5).First();
        
        var searchResults = new List<DbQueryResult<ICollection<TModel>>>();
        
        // Act
        foreach (var searchModel in this.CreateSearchModels(model))
        {
            searchResults.Add(await this.repository.SearchAsync(searchModel));
        }

        // Assert
        foreach (var result in searchResults)
        {
            result.Status.Should().Be(DbQueryResultStatus.Ok);
            result.Result.Should().HaveCount(1);
            result.Result.First().Should().BeEquivalentTo(model);
        }
    }

    [Test]
    public async Task SearchAsync_NotFound()
    {
        // Arrange
        var searchModel = this.CreateSearchModels(this.CreateModel()).First();

        // Act
        var searchResult = await this.repository.SearchAsync(searchModel);

        // Assert
        searchResult.Status.Should().Be(DbQueryResultStatus.NotFound);
    }

    [Test]
    public async Task RemoveAsync_Success()
    {
        // Arrange
        const int modelsCount = 5;
        
        var models = await this.CreateModelsInRepository(modelsCount);
        var model = models.Skip(2).First();

        // Act
        var removeResult = await this.repository.RemoveAsync(model.Id);

        // Assert
        removeResult.Status.Should().Be(DbQueryResultStatus.Ok);

        var searchResult = await this.repository.SearchAsync(this.CreateSearchModels(model, byIdOnly: true).First());
        searchResult.Status.Should().Be(DbQueryResultStatus.NotFound);

        var fullSearchResult = await this.repository.SearchAsync(new TSearchModel());
        fullSearchResult.Status.Should().Be(DbQueryResultStatus.Ok);
        fullSearchResult.Result.Count.Should().Be(modelsCount - 1);
    }

    [Test]
    public async Task UpdateAsync_Success()
    {
        // Arrange
        var models = await this.CreateModelsInRepository(5);
        var model = models.Skip(1).First();
        
        var updateModel = this.CreateModel();
        updateModel.Id = model.Id;
        
        // Act
        var updateResult = await this.repository.UpdateAsync(updateModel);

        // Assert
        updateResult.Status.Should().Be(DbQueryResultStatus.Ok);

        var searchResult = await this.repository.SearchAsync(this.CreateSearchModels(model, byIdOnly: true).First());
        searchResult.Status.Should().Be(DbQueryResultStatus.Ok);

        searchResult.Result.Should().HaveCount(1);
        searchResult.Result.First().Should().BeEquivalentTo(this.ApplyUpdate(model, updateModel));
    }
    
    protected abstract TModel CreateModel();
    
    protected abstract IEnumerable<TSearchModel> CreateSearchModels(TModel model, bool byIdOnly = false);
    
    protected abstract TModel ApplyUpdate(TModel model, TModel update);

    private async Task<ICollection<TModel>> CreateModelsInRepository(int count)
    {
        var models = new List<TModel>(count);
        
        for (var i = 0; i < count; i++)
        {
            var model = this.CreateModel();
        
            var addResult = await this.repository.AddAsync(model);
            addResult.EnsureSuccess();
        
            model.Id = addResult.Result;
            this.currentEntities.Add(addResult.Result); 
            
            models.Add(model);
        }

        return models;
    }
}