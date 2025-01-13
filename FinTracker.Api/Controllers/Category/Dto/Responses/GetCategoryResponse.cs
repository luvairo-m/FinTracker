using System;

namespace FinTracker.Api.Controllers.Category.Dto.Responses;

public class GetCategoryResponse
{
    public Guid Id { get; init; }
    
    public string Title { get; init; }
    
    public string Description { get; init; }
}