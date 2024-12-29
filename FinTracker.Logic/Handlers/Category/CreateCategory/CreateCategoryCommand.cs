using FinTracker.Logic.Models.Category;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.CreateCategory;

public class CreateCategoryCommand : IRequest<CreateCategoryModel>
{
    public CreateCategoryCommand(string title)
    {
        Title = title;
    }
    
    public string Title { get; set; }
}