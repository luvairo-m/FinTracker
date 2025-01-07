using FinTracker.Dal.Logic.Connections;
using FinTracker.Dal.Models.Payments;
using FinTracker.Dal.Repositories;
using NUnit.Framework;
using Vostok.Logging.Abstractions;

namespace FinTracker.Integration.Tests.Repositories;

[TestFixture]
public class PaymentRepositoryTests : RepositoryBaseTests<Payment, PaymentSearch>
{
    public PaymentRepositoryTests() 
        : base(new PaymentRepository(
            new SqlConnectionFactory(TestCredentials.FinTrackerConnectionString), 
            new SilentLog()))
    {
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
            CategoryId = Guid.NewGuid()
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
            CategoryId = update.CategoryId ?? model.CategoryId,
            
            // Нельзя обновлять.
            Amount = model.Amount,
            Type = model.Type,
            Date = model.Date,
            BillId = model.BillId,
        };
    }
}