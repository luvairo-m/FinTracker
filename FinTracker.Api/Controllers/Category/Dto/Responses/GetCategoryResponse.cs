using System;

namespace FinTracker.Api.Controllers.Category.Dto.Responses;

public record struct GetCategoryResponse
{
    public required Guid CategoryId { get; init; }
    
    public required string Title { get; init; }
}