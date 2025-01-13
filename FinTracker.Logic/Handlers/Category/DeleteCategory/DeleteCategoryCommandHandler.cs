using FinTracker.Dal.Repositories.Categories;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.DeleteCategory;

internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly ICategoryRepository categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository;
    }
    
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var deletionCategoryResult = await categoryRepository.RemoveAsync(request.CategoryId);
        deletionCategoryResult.EnsureSuccess();
    }
}