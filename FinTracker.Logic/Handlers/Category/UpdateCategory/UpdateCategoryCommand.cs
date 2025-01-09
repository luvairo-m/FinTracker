using MediatR;

namespace FinTracker.Logic.Handlers.Category.UpdateCategory;

public class UpdateCategoryCommand : IRequest
{
    public UpdateCategoryCommand(Guid id, string title, string description)
    {
        Id = id;
        Title = title;
        Description = description;
    }
    
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }
}