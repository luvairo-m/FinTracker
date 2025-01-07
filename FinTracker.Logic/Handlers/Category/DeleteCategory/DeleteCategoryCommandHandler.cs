using FinTracker.Dal.Repositories;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.DeleteCategory;

internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly CategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(CategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        await _categoryRepository.RemoveAsync(request.CategoryId);
    }
}