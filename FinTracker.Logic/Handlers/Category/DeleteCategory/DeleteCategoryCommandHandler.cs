using MediatR;

namespace FinTracker.Logic.Handlers.Category.DeleteCategory;

internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    public Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}