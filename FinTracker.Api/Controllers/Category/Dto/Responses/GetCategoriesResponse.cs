using System.Collections.Generic;

namespace FinTracker.Api.Controllers.Category.Dto.Responses;

public record struct GetCategoriesResponse
{
    public required ICollection<GetCategoryResponse> Categories { get; set; }
}