using FinTracker.Dal.Repositories;
using FinTracker.Logic.Models.Category;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.CreateCategory;

internal class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryModel>
{
    private readonly CategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(CategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CreateCategoryModel> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Dal.Models.Categories.Category
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description
        };

        var categoryId = await _categoryRepository.AddAsync(category);

        return new CreateCategoryModel { CategoryId = categoryId.Result };
    }
}