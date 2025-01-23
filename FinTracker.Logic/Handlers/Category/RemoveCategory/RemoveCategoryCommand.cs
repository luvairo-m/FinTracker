using MediatR;

namespace FinTracker.Logic.Handlers.Category.RemoveCategory;

public class RemoveCategoryCommand : IRequest
{
    public Guid Id { get; set; }
}