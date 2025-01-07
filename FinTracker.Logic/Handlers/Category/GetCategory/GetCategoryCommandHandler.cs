using AutoMapper;
using FinTracker.Dal.Models.Categories;
using FinTracker.Dal.Repositories;
using FinTracker.Logic.Models.Category;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.GetCategory;

internal class GetCategoryCommandHandler : IRequestHandler<GetCategoryCommand, GetCategoryModel>
{
    private readonly CategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryCommandHandler(CategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<GetCategoryModel> Handle(GetCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.SearchAsync(new CategorySearch { Id = request.CategoryId });

        return _mapper.Map<GetCategoryModel>(category.Result);
    }
}