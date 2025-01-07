using FinTracker.Dal.Logic.Connections;
using FinTracker.Dal.Models.Bills;
using FinTracker.Dal.Repositories;
using NUnit.Framework;
using Vostok.Logging.Abstractions;

namespace FinTracker.Integration.Tests.Repositories;

[TestFixture]
public class BillRepositoryTests : RepositoryBaseTests<Bill, BillSearch>
{
    private readonly Random random = new();

    public BillRepositoryTests()
        : base(new BillRepository(
            new SqlConnectionFactory(TestCredentials.FinTrackerConnectionString), 
            new SilentLog()))
    {
    }
    
    protected override Bill CreateModel()
    {
        return new Bill
        {
            Balance = random.Next(0, int.MaxValue),
            Title = Guid.NewGuid().ToString(),
            Description = Guid.NewGuid().ToString(),
            CurrencyId = Guid.NewGuid()
        };
    }

    protected override BillSearch CreateSearchModel(Bill model, bool byIdOnly = false)
    {
        var search = new BillSearch { Id = model.Id };
        
        if (!byIdOnly)
        {
            search.TitleSubstring = model.Title;
            search.CurrencyId = model.CurrencyId;
        }

        return search;
    }

    protected override Bill ApplyUpdate(Bill model, Bill update)
    {
        return new Bill
        {
            Id = model.Id,
            Balance = update.Balance ?? model.Balance,
            Title = update.Title ?? model.Title,
            Description = update.Description ?? model.Description,
            CurrencyId = update.CurrencyId ?? model.CurrencyId
        };
    }
}