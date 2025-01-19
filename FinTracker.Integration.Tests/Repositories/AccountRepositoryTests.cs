using FinTracker.Dal.Logic.Connections;
using FinTracker.Dal.Models.Accounts;
using FinTracker.Dal.Repositories.Accounts;
using NUnit.Framework;
using Vostok.Logging.Abstractions;

namespace FinTracker.Integration.Tests.Repositories;

[TestFixture]
public class AccountRepositoryTests : RepositoryBaseTests<Account, AccountSearch>
{
    private readonly Random random = new();

    public AccountRepositoryTests()
    {
        this.repository = new AccountRepository(
            new SqlConnectionFactory(TestCredentials.FinTrackerConnectionString),
            new SilentLog());
    }
    
    protected override Account CreateModel()
    {
        return new Account
        {
            Balance = random.Next(0, int.MaxValue),
            Title = Guid.NewGuid().ToString(),
            Description = Guid.NewGuid().ToString(),
            CurrencyId = Guid.NewGuid()
        };
    }

    protected override AccountSearch CreateSearchModel(Account model, bool byIdOnly = false)
    {
        var search = new AccountSearch { Id = model.Id };
        
        if (!byIdOnly)
        {
            search.TitleSubstring = model.Title;
            search.CurrencyId = model.CurrencyId;
        }

        return search;
    }

    protected override Account ApplyUpdate(Account model, Account update)
    {
        return new Account
        {
            Id = model.Id,
            Balance = update.Balance ?? model.Balance,
            Title = update.Title ?? model.Title,
            Description = update.Description ?? model.Description,
            CurrencyId = update.CurrencyId ?? model.CurrencyId
        };
    }
}