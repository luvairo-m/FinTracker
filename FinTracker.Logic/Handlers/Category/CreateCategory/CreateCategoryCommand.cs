using FinTracker.Logic.Models.Category;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.CreateCategory;

public class CreateCategoryCommand : IRequest<CreateCategoryModel>
{
    public string Title { get; set; }

    public string Description { get; set; }
}