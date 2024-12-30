using FinTracker.Dal.Logic;
using FinTracker.Dal.Models;
using FinTracker.Dal.Repositories;
using FinTracker.Integration.Tests.Utils;
using FluentAssertions;
using NUnit.Framework;

namespace FinTracker.Integration.Tests.Repositories;

public abstract class RepositoryBaseTests<TModel, TSearchModel, TUpdateModel>
    where TModel : class, IEntity
{
    protected DatabaseInitializer databaseInitializer;
    protected RepositoryBase<TModel, TSearchModel, TUpdateModel> repository;
    protected readonly List<Guid> addedIds = new();
    
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
        foreach (var id in addedIds)
        {
            await this.repository.RemoveAsync(id);
        }
    }

    [Test]
    public async Task SearchAsync_Success()
    {
        // Arrange
        var model = (await this.CreateModelsInRepository(5)).Skip(2).First();
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
            result.Result.Should().HaveCount(1).And.ContainSingle(mdl => this.Equals(model, mdl));
        }
    }

    [Test]
    public async Task SearchAsync_NotFound()
    {
        // Arrange
        var searchModel = this.CreateSearchModels(null).First();

        // Act
        var searchResult = await this.repository.SearchAsync(searchModel);

        // Assert
        searchResult.Status.Should().Be(DbQueryResultStatus.NotFound);
    }

    [Test]
    public async Task RemoveAsync_Success()
    {
        // Arrange
        var model = (await this.CreateModelsInRepository(1)).First();

        // Act
        var removeResult = await this.repository.RemoveAsync(model.Id);

        // Assert
        removeResult.Status.Should().Be(DbQueryResultStatus.Ok);

        var searchResult = await this.repository.SearchAsync(this.CreateSearchModels(model).First());
        searchResult.Status.Should().Be(DbQueryResultStatus.NotFound);
    }

    [Test]
    public async Task UpdateAsync_Success()
    {
        // Arrange
        var model = (await this.CreateModelsInRepository(5)).Skip(3).First();
        var (updateModel, updatedModel) = this.CreateMetaForUpdate(model);
        
        // Act
        var updateResult = await this.repository.UpdateAsync(model.Id, updateModel);

        // Assert
        updateResult.Status.Should().Be(DbQueryResultStatus.Ok);

        var searchResult = await this.repository.SearchAsync(this.CreateSearchModels(model, byIdOnly: true).First());
        
        searchResult.Status.Should().Be(DbQueryResultStatus.Ok);
        searchResult.Result.Should().HaveCount(1).And.ContainSingle(mdl => this.Equals(mdl, updatedModel));
    }
    
    protected abstract Task<ICollection<TModel>> CreateModelsInRepository(int count);

    protected abstract IEnumerable<TSearchModel> CreateSearchModels(TModel model, bool byIdOnly = false);
    
    protected abstract (TUpdateModel update, TModel updated) CreateMetaForUpdate(TModel model);
    
    protected abstract bool Equals(TModel first, TModel second);
}