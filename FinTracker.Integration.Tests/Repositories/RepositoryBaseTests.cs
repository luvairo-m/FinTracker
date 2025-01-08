using System.Data.SqlTypes;
using FinTracker.Dal.Logic;
using FinTracker.Dal.Models.Abstractions;
using FinTracker.Dal.Repositories;
using FinTracker.Integration.Tests.Utils;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

namespace FinTracker.Integration.Tests.Repositories;

public abstract class RepositoryBaseTests<TModel, TSearchModel>
    where TModel : IEntity
    where TSearchModel : new()
{
    protected readonly List<Guid> currentEntities = new();
    protected RepositoryBase<TModel, TSearchModel> repository;

    private readonly DatabaseInitializer databaseInitializer = new(
        TestCredentials.FinTrackerConnectionString,
        Directory.GetFiles($"Scripts/{typeof(TModel).Name}/Create", "*.sql", SearchOption.AllDirectories),
        Directory.GetFiles($"Scripts/{typeof(TModel).Name}/Drop", "*.sql", SearchOption.AllDirectories));

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await this.databaseInitializer.DropTablesAsync();
        (await this.databaseInitializer.CreateTablesAsync()).EnsureSuccess();
        
        AssertionOptions.AssertEquivalencyUsing(
            options => options
                .Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, 1.Seconds()))
                .WhenTypeIs<DateTime>());
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
        
        // Act
        var searchResult = await this.repository.SearchAsync(this.CreateSearchModel(model));
        
        // Assert
        searchResult.Status.Should().Be(DbQueryResultStatus.Ok);
        searchResult.Result.Should().HaveCount(1);
        searchResult.Result.First().Should().BeEquivalentTo(model);
    }

    [Test]
    public async Task SearchAsync_Pagination_Success()
    {
        // Arrange
        var models = (await this.CreateModelsInRepository(5)).OrderBy(mdl => (SqlGuid)mdl.Id).ToArray();
        
        // Act
        var singleResult = await this.repository.SearchAsync(new TSearchModel(), skip: 2, take: 1);
        var multipleResult = await this.repository.SearchAsync(new TSearchModel(), skip: 3, take: 2);
        
        // Assert
        singleResult.Status.Should().Be(DbQueryResultStatus.Ok);
        singleResult.Result.Should().HaveCount(1);
        singleResult.Result.First().Should().BeEquivalentTo(models.Skip(2).Take(1).First());

        multipleResult.Status.Should().Be(DbQueryResultStatus.Ok);
        multipleResult.Result.Should().HaveCount(2);
        multipleResult.Result.Select(mdl => mdl.Id).Should().ContainInOrder(models.Skip(3).First().Id, models.Last().Id);
    }

    [Test]
    public async Task SearchAsync_NotFound()
    {
        // Arrange
        var searchModel = this.CreateSearchModel(this.CreateModel());

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

        var searchResult = await this.repository.SearchAsync(this.CreateSearchModel(model, byIdOnly: true));
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

        var searchResult = await this.repository.SearchAsync(this.CreateSearchModel(model, byIdOnly: true));
        searchResult.Status.Should().Be(DbQueryResultStatus.Ok);

        searchResult.Result.Should().HaveCount(1);
        searchResult.Result.First().Should().BeEquivalentTo(this.ApplyUpdate(model, updateModel));
    }
    
    protected abstract TModel CreateModel();
    
    protected abstract TSearchModel CreateSearchModel(TModel model, bool byIdOnly = false);
    
    protected abstract TModel ApplyUpdate(TModel model, TModel update);

    protected async Task<ICollection<TModel>> CreateModelsInRepository(int count)
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