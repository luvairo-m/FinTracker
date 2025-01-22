using FinTracker.Dal.Repositories.Categories;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.RemoveCategory;

// ReSharper disable once UnusedType.Global
internal class RemoveCategoryCommandHandler : IRequestHandler<RemoveCategoryCommand>
{
    private readonly ICategoryRepository categoryRepository;

    public RemoveCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository;
    }
    
    public async Task Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
    {
        var deletionCategoryResult = await categoryRepository.RemoveAsync(request.Id);
        deletionCategoryResult.EnsureSuccess();
    }
}