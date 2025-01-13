using FinTracker.Dal.Repositories.Categories;
using FinTracker.Logic.Models.Category;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.CreateCategory;

internal class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryModel>
{
    private readonly ICategoryRepository categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository;
    }

    public async Task<CreateCategoryModel> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var newCategory = new Dal.Models.Categories.Category
        {
            Title = request.Title,
            Description = request.Description
        };

        var addedCategoryResult = await categoryRepository.AddAsync(newCategory);
        addedCategoryResult.EnsureSuccess();

        return new CreateCategoryModel { CategoryId = addedCategoryResult.Result };
    }
}