using MediatR;

namespace FinTracker.Logic.Handlers.Category.UpdateCategory;

public class UpdateCategoryCommand : IRequest
{
    public UpdateCategoryCommand(Guid categoryId, string title, string description)
    {
        CategoryId = categoryId;
        Title = title;
        Description = description;
    }
    
    public Guid CategoryId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
}