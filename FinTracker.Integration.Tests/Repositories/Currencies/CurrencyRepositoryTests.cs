using System.Text;
using FinTracker.Dal.Logic;
using FinTracker.Dal.Logic.Connections;
using FinTracker.Dal.Models.Currencies;
using FinTracker.Dal.Repositories.Currencies;
using FinTracker.Integration.Tests.Utils;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Logging.Abstractions;

namespace FinTracker.Integration.Tests.Repositories.Currencies;

[TestFixture]
public class CurrencyRepositoryTests : RepositoryBaseTests<Currency, CurrencySearch>
{
    private const int maxSignLength = 6;
    
    private static readonly Random random = new();
    
    public CurrencyRepositoryTests()
    {
        this.databaseInitializer = new DatabaseInitializer(
            TestCredentials.FinTrackerConnectionString,
            Directory.GetFiles("Scripts/Currencies/Create", "*.sql", SearchOption.AllDirectories),
            Directory.GetFiles("Scripts/Currencies/Drop", "*.sql", SearchOption.AllDirectories));

        this.repository = new CurrencyRepository(
            new SqlConnectionFactory(TestCredentials.FinTrackerConnectionString), 
            new SilentLog());
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

    protected override IEnumerable<CurrencySearch> CreateSearchModels(Currency model, bool byIdOnly = false)
    {
        if (byIdOnly)
        {
            yield return new CurrencySearch { Id = model.Id };
            yield break;
        }
        
        yield return new CurrencySearch
        {
            Id = model.Id,
            TitleSubstring = model.Title
        };
        
        yield return new CurrencySearch
        {
            Id = model.Id
        };
        
        yield return new CurrencySearch
        {
            TitleSubstring = model.Title
        };
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