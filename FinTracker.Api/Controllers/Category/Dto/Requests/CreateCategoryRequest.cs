using System.ComponentModel.DataAnnotations;

namespace FinTracker.Api.Controllers.Category.Dto.Requests;

public class CreateCategoryRequest
{
    [Required]
    [MaxLength(128)]
    public string Title { get; init; }

    [MaxLength(1024)]
    public string Description { get; init; }
}