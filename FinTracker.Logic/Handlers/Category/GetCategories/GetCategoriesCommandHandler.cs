using AutoMapper;
using FinTracker.Dal.Models.Categories;
using FinTracker.Dal.Repositories;
using FinTracker.Dal.Repositories.Categories;
using FinTracker.Logic.Models.Category;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.GetCategories;

internal class GetCategoriesCommandHandler : IRequestHandler<GetCategoriesCommand, GetCategoriesModel>
{
    private readonly CategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoriesCommandHandler(CategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<GetCategoriesModel> Handle(GetCategoriesCommand request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.SearchAsync(new CategorySearch());

        return new GetCategoriesModel
        {
            Categories = _mapper.Map<ICollection<GetCategoryModel>>(categories.Result)
        };
    }
}