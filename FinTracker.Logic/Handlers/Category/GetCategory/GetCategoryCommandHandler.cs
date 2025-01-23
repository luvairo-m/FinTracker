using AutoMapper;
using FinTracker.Dal.Models.Categories;
using FinTracker.Dal.Repositories.Categories;
using FinTracker.Logic.Models.Category;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.GetCategory;

// ReSharper disable once UnusedType.Global
internal class GetCategoryCommandHandler : IRequestHandler<GetCategoryCommand, GetCategoryModel>
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;

    public GetCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
    }

    public async Task<GetCategoryModel> Handle(GetCategoryCommand request, CancellationToken cancellationToken)
    {
        var gettingCategoriesResult = await categoryRepository.SearchAsync(mapper.Map<CategorySearch>(request));
        gettingCategoriesResult.EnsureSuccess();
        
        var category = gettingCategoriesResult.Result.FirstOrDefault();
        
        return mapper.Map<GetCategoryModel>(category);
    }
}