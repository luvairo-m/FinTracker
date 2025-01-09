using AutoMapper;
using FinTracker.Dal.Models.Categories;
using FinTracker.Dal.Repositories.Categories;
using FinTracker.Logic.Models.Category;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.GetCategory;

internal class GetCategoryCommandHandler : IRequestHandler<GetCategoryCommand, GetCategoryModel>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<GetCategoryModel> Handle(GetCategoryCommand request, CancellationToken cancellationToken)
    {
        var gettingCategoriesResult = await _categoryRepository.SearchAsync(new CategorySearch { Id = request.CategoryId });
        gettingCategoriesResult.EnsureSuccess();
        
        var category = gettingCategoriesResult.Result.FirstOrDefault();
        
        return _mapper.Map<GetCategoryModel>(category);
    }
}