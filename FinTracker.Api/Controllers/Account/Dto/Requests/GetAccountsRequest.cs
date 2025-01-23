using System;
using System.ComponentModel.DataAnnotations;

namespace FinTracker.Api.Controllers.Account.Dto.Requests;

public class GetAccountsRequest
{
    public Guid? Id { get; init; }

    [MaxLength(128)]
    public string TitleSubstring { get; init; }
    
    public Guid? CurrencyId { get; init; }
}