using FinTracker.Logic.Models.Category;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.GetCategory;

public class GetCategoryCommand : IRequest<GetCategoryModel>
{
    public GetCategoryCommand(Guid categoryId)
    {
        CategoryId = categoryId;
    }
    
    public Guid CategoryId { get; set; }
}