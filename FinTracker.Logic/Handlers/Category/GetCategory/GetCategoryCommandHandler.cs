using FinTracker.Logic.Models.Category;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.GetCategory;

internal class GetCategoryCommandHandler : IRequestHandler<GetCategoryCommand, GetCategoryModel>
{
    public Task<GetCategoryModel> Handle(GetCategoryCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}