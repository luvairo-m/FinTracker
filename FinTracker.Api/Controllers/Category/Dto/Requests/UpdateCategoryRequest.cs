namespace FinTracker.Api.Controllers.Category.Dto.Requests;

public record struct UpdateCategoryRequest
{
    public required string Title { get; init; }

    public required string Description { get; init; }
}