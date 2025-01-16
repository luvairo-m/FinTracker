using System;

namespace FinTracker.Api.Controllers.Bill.Dto.Responses;

public class GetAccountResponse
{
    public Guid Id { get; init; }

    public string Title { get; init; }

    public decimal Balance { get; init; }
    
    public string Description { get; init; }
    
    public Guid CurrencyId { get; init; }
}