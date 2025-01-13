using System.Collections.Generic;

namespace FinTracker.Api.Controllers.Category.Dto.Responses;

public class GetCategoriesResponse
{
    public ICollection<GetCategoryResponse> Categories { get; init; }
}