using System.ComponentModel.DataAnnotations;

namespace FinTracker.Api.Controllers.Bill.Dto.Requests;

public record struct CreateBillRequest
{
    [Required]
    [MaxLength(128)]
    public required string Title { get; init; }

    [Required]
    public required decimal Balance { get; init; }

    [MaxLength(1024)]
    public required string Description { get; init; }
}