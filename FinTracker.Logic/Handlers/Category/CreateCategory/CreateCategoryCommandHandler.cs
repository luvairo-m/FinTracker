using AutoMapper;
using FinTracker.Dal.Repositories.Categories;
using FinTracker.Logic.Models.Category;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.CreateCategory;

// ReSharper disable once UnusedType.Global
internal class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryModel>
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
    }

    public async Task<CreateCategoryModel> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var newCategory = mapper.Map<Dal.Models.Categories.Category>(request);

        var addedCategoryResult = await categoryRepository.AddAsync(newCategory);
        addedCategoryResult.EnsureSuccess();

        return new CreateCategoryModel { CategoryId = addedCategoryResult.Result };
    }
}