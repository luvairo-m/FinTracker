using MediatR;

namespace FinTracker.Logic.Handlers.Category.UpdateCategory;

public class UpdateCategoryCommand : IRequest
{
    public UpdateCategoryCommand(Guid categoryId, string title)
    {
        CategoryId = categoryId;
        Title = title;
    }
    
    public Guid CategoryId { get; set; }

    public string Title { get; set; }
}