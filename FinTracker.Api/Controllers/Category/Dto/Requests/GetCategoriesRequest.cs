using System.ComponentModel.DataAnnotations;

namespace FinTracker.Api.Controllers.Category.Dto.Requests;

public class GetCategoriesRequest
{
    [MaxLength(128)]
    public string TitleSubstring { get; set; }
}