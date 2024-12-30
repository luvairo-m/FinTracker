using FinTracker.Logic.Models.Category;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.CreateCategory;

internal class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryModel>
{
    public Task<CreateCategoryModel> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}