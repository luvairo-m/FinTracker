using FinTracker.Dal.Logic;
using FinTracker.Dal.Logic.Connections;
using FinTracker.Dal.Models.Payments;
using FinTracker.Dal.Repositories;
using FinTracker.Dal.Repositories.Payments;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Logging.Abstractions;

namespace FinTracker.Integration.Tests.Repositories;

[TestFixture]
public class PaymentRepositoryTests : RepositoryBaseTests<Payment, PaymentSearch>
{
    private readonly IPaymentRepository paymentRepository;
    
    public PaymentRepositoryTests()
    {
        this.paymentRepository = new PaymentRepository(
            new SqlConnectionFactory(TestCredentials.FinTrackerConnectionString),
            new SilentLog());

        this.repository = (RepositoryBase<Payment, PaymentSearch>)this.paymentRepository;
    }

    [Test]
    public async Task SearchAsync_Categories_Success()
    {
        // Arrange
        var payments = await this.CreateModelsInRepository(2);
        
        var payment1 = payments.First();
        var payment2 = payments.Skip(1).First();
        
        // Act
        var pay1SearchResult = await this.paymentRepository.SearchAsync(new PaymentSearch { Id = payment1.Id });
        var pay2SearchResult = await this.paymentRepository.SearchAsync(new PaymentSearch { Id = payment2.Id });
        
        // Assert
        pay1SearchResult.Status.Should().Be(DbQueryResultStatus.Ok);
        pay2SearchResult.Status.Should().Be(DbQueryResultStatus.Ok);
        
        pay1SearchResult.Result.Should().HaveCount(1);
        pay2SearchResult.Result.Should().HaveCount(1);
        
        pay1SearchResult.Result.First().Categories.Should().BeEquivalentTo(payment1.Categories);
        pay2SearchResult.Result.First().Categories.Should().BeEquivalentTo(payment2.Categories);
    }

    [Test]
    public async Task UpdateCategoriesAsync_Success()
    {
        // Arrange
        var payment = (await this.CreateModelsInRepository(1)).First();
        var newCategoryId = Guid.NewGuid();
        
        // Act
        var updateResult = await this.paymentRepository.UpdateCategoriesAsync(
            payment.Id, 
            addCategories: new List<Guid> { newCategoryId }, 
            removeCategories: payment.Categories);
        
        // Assert
        updateResult.Status.Should().Be(DbQueryResultStatus.Ok);

        var searchResult = await this.paymentRepository.SearchAsync(new PaymentSearch { Id = payment.Id });
        searchResult.Status.Should().Be(DbQueryResultStatus.Ok);

        searchResult.Result.Should().HaveCount(1);
        searchResult.Result.First().Categories.Should().HaveCount(1).And.Contain(newCategoryId);
    }
    
    protected override Payment CreateModel()
    {
        return new Payment
        {
            Title = Guid.NewGuid().ToString(),
            Description = Guid.NewGuid().ToString(),
            Amount = 15_000,
            Type = OperationType.Income,
            Date = DateTime.UtcNow,
            BillId = Guid.NewGuid(),
            Categories = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
        };
    }

    protected override PaymentSearch CreateSearchModel(Payment model, bool byIdOnly = false)
    {
        var search = new PaymentSearch { Id = model.Id };
        
        if (!byIdOnly)
        {
            search.TitleSubstring = model.Title;
            search.BillId = model.BillId;
            search.Months = new[] { model.Date!.Value.Month };
            search.Years = new[] { model.Date!.Value.Year };
            search.Types = new[] { model.Type!.Value };
            search.MinAmount = model.Amount;
            search.MaxAmount = model.Amount;
            search.MinDate = model.Date!.Value.AddDays(-5);
            search.MaxDate = model.Date!.Value.AddDays(5);
            search.Categories = new List<Guid> { model.Categories.First() };
        }

        return search;
    }

    protected override Payment ApplyUpdate(Payment model, Payment update)
    {
        return new Payment
        {
            Id = update.Id,
            Title = update.Title ?? model.Title,
            Description = update.Description ?? model.Description,
            Amount = update.Amount ?? model.Amount,
            Type = update.Type ?? model.Type,
            Date = update.Date ?? model.Date,
            BillId = update.BillId ?? model.BillId,
            
            // Обновляется через специальный метод.
            Categories = model.Categories
        };
    }
}