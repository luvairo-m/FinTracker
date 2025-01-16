using System.ComponentModel.DataAnnotations;

namespace FinTracker.Api.Controllers.Category.Dto.Requests;

public class GetCategoriesRequest
{
    [MaxLength(512)]
    public string TitleSubstring { get; set; }
}