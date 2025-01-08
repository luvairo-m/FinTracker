using System;

namespace FinTracker.Api.Controllers.Category.Dto.Responses;

public record struct GetCategoryResponse
{
    public required Guid Id { get; init; }
    
    public required string Title { get; init; }
    
    public required string Description { get; init; }
}