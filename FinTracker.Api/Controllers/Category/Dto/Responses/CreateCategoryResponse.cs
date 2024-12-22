using System;

namespace FinTracker.Api.Controllers.Category.Dto.Responses;

public record struct CreateCategoryResponse
{
    public required Guid CategoryId { get; init; }
}