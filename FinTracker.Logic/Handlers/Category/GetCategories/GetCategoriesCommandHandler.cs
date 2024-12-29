using FinTracker.Logic.Models.Category;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.GetCategories;

internal class GetCategoriesCommandHandler : IRequestHandler<GetCategoriesCommand, GetCategoriesModel>
{
    public Task<GetCategoriesModel> Handle(GetCategoriesCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}