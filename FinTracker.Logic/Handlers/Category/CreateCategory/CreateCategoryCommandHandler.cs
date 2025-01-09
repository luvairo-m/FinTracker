using FinTracker.Dal.Repositories.Categories;
using FinTracker.Logic.Models.Category;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.CreateCategory;

internal class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryModel>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CreateCategoryModel> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var newCategory = new Dal.Models.Categories.Category
        {
            Title = request.Title,
            Description = request.Description
        };

        var addedCategoryResult = await _categoryRepository.AddAsync(newCategory);
        
        addedCategoryResult.EnsureSuccess();

        return new CreateCategoryModel { CategoryId = addedCategoryResult.Result };
    }
}