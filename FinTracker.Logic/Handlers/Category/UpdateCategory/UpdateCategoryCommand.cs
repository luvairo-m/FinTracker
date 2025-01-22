using MediatR;

namespace FinTracker.Logic.Handlers.Category.UpdateCategory;

public class UpdateCategoryCommand : IRequest
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
}