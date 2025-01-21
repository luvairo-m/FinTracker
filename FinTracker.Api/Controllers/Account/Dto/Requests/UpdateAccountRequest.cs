using System;
using System.ComponentModel.DataAnnotations;

namespace FinTracker.Api.Controllers.Account.Dto.Requests;

public class UpdateAccountRequest
{
    [MaxLength(128)]
    public string Title { get; init; }
    
    [MaxLength(1024)]
    public string Description { get; init; }
    
    [Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public decimal? Balance { get; init; }
    
    public Guid? CurrencyId { get; init; }
}