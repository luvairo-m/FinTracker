using FinTracker.Dal.Models.Currencies;
using FinTracker.Dal.Repositories.Currencies;
using FinTracker.Integration.Tests.Utils;
using Vostok.Logging.Abstractions;

namespace FinTracker.Integration.Tests.Repositories.Currencies;

public class CurrencyRepositoryTests : RepositoryBaseTests<Currency, CurrencySearch, CurrencyUpdate>
{
    private static readonly Random random = new();
    
    public CurrencyRepositoryTests()
    {
        this.databaseInitializer = new DatabaseInitializer(
            TestCredentials.FinTrackerConnectionString,
            Directory.GetFiles("Scripts/Currencies/Create", "*.sql", SearchOption.AllDirectories),
            Directory.GetFiles("Scripts/Currencies/Drop", "*.sql", SearchOption.AllDirectories));

        this.repository = new CurrencyRepository(TestCredentials.FinTrackerConnectionString, new SilentLog());
    }

    protected override async Task<ICollection<Currency>> CreateModelsInRepository(int count)
    {
        var categories = Enumerable
            .Range(0, count)
            .Select(_ =>
            {
                var rawTitle = Guid.NewGuid().ToString(); 
                
                return new Currency
                {
                    Title = rawTitle[..(rawTitle.Length / 2)],
                    Sign = ((char)random.Next(128)).ToString()
                };
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

    protected override IEnumerable<CurrencySearch> CreateSearchModels(Currency model, bool byIdOnly = false)
    {
        if (model == null)
        {
            yield return new CurrencySearch();
            yield break;
        }

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

    protected override (CurrencyUpdate update, Currency updated) CreateMetaForUpdate(Currency model)
    {
        var update = new CurrencyUpdate
        {
            Title = model.Title + model.Title,
            Sign = model.Sign + model.Sign
        };

        var updated = new Currency
        {
            Id = model.Id,
            Title = update.Title,
            Sign = update.Sign
        };

        return (update, updated);
    }

    protected override bool Equals(Currency first, Currency second)
    {
        return first.Id == second.Id && first.Title == second.Title && first.Sign == second.Sign;
    }
}