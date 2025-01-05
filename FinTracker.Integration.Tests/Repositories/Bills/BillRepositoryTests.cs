using FinTracker.Dal.Logic.Connections;
using FinTracker.Dal.Models.Bills;
using FinTracker.Dal.Repositories.Bills;
using FinTracker.Integration.Tests.Utils;
using NUnit.Framework;
using Vostok.Logging.Abstractions;

namespace FinTracker.Integration.Tests.Repositories.Bills;

[TestFixture]
public class BillRepositoryTests : RepositoryBaseTests<Bill, BillSearch>
{
    private readonly Random random = new();

    public BillRepositoryTests()
    {
        this.databaseInitializer = new DatabaseInitializer(
            TestCredentials.FinTrackerConnectionString,
            Directory.GetFiles("Scripts/Bills/Create", "*.sql", SearchOption.AllDirectories),
            Directory.GetFiles("Scripts/Bills/Drop", "*.sql", SearchOption.AllDirectories));

        this.repository = new BillRepository(
            new SqlConnectionFactory(TestCredentials.FinTrackerConnectionString), 
            new SilentLog());
    }
    
    protected override Bill CreateModel()
    {
        return new Bill
        {
            Balance = random.Next(0, int.MaxValue),
            Title = Guid.NewGuid().ToString()
        };
    }

    protected override IEnumerable<BillSearch> CreateSearchModels(Bill model, bool byIdOnly = false)
    {
        if (byIdOnly)
        {
            yield return new BillSearch { Id = model.Id };
            yield break;
        }
        
        yield return new BillSearch
        {
            Id = model.Id,
            TitleSubstring = model.Title
        };
        
        yield return new BillSearch
        {
            Id = model.Id
        };
        
        yield return new BillSearch
        {
            TitleSubstring = model.Title
        };
    }

    protected override Bill ApplyUpdate(Bill model, Bill update)
    {
        return new Bill
        {
            Id = model.Id,
            Balance = update.Balance ?? model.Balance,
            Title = update.Title ?? model.Title
        };
    }
}