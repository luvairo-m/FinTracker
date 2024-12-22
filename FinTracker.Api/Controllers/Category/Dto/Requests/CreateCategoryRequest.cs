using System.ComponentModel.DataAnnotations;

namespace FinTracker.Api.Controllers.Category.Dto.Requests;

public record struct CreateCategoryRequest
{
    [Required]
    [MaxLength(128)]
    public required string Title { get; init; }
}