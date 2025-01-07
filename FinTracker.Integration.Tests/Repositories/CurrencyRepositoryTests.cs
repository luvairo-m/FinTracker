using System.Text;
using FinTracker.Dal.Logic;
using FinTracker.Dal.Logic.Connections;
using FinTracker.Dal.Models.Currencies;
using FinTracker.Dal.Repositories;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Logging.Abstractions;

namespace FinTracker.Integration.Tests.Repositories;

[TestFixture]
public class CurrencyRepositoryTests : RepositoryBaseTests<Currency, CurrencySearch>
{
    private const int maxSignLength = 6;
    
    private static readonly Random random = new();
    
    public CurrencyRepositoryTests() 
        : base(new CurrencyRepository(
            new SqlConnectionFactory(TestCredentials.FinTrackerConnectionString), 
            new SilentLog()))
    {
    }

    [Test]
    public async Task AddAsync_DuplicateSign()
    {
        // Arrange
        var currency = new Currency { Sign = "$", Title = "American dollar" };
        
        // Act
        var originalAddResult = await this.repository.AddAsync(currency);
        originalAddResult.EnsureSuccess();
        
        this.currentEntities.Add(originalAddResult.Result);
        
        var duplicateAddResult = await this.repository.AddAsync(currency);
        
        // Assert
        duplicateAddResult.Status.Should().Be(DbQueryResultStatus.Conflict);
    }

    protected override Currency CreateModel()
    { 
        var rawTitle = Guid.NewGuid().ToString(); 
        
        return new Currency 
        { 
            Title = rawTitle[..(rawTitle.Length / 2)], 
            Sign = CreateRandomSign(maxSignLength)
        };
    }

    protected override CurrencySearch CreateSearchModel(Currency model, bool byIdOnly = false)
    {
        var search = new CurrencySearch { Id = model.Id };

        if (!byIdOnly)
        {
            search.TitleSubstring = model.Title;
        }

        return search;
    }

    protected override Currency ApplyUpdate(Currency model, Currency update)
    {
        return new Currency
        {
            Id = model.Id,
            Title = update.Title ?? model.Title,
            Sign = update.Sign ?? model.Sign
        };
    }

    private static string CreateRandomSign(int length)
    {
        var signBuilder = new StringBuilder(length);

        for (var i = 0; i < length; i++)
        {
            signBuilder.Append((char)random.Next(128));
        }

        return signBuilder.ToString();
    }
}