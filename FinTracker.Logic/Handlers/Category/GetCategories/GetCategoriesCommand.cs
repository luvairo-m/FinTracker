using FinTracker.Logic.Models.Category;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.GetCategories;

public class GetCategoriesCommand : IRequest<ICollection<GetCategoryModel>>
{
    public string TitleSubstring { get; set; }
}