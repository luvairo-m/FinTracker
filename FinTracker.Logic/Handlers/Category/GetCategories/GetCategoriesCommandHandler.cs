using AutoMapper;
using FinTracker.Dal.Models.Categories;
using FinTracker.Dal.Repositories.Categories;
using FinTracker.Logic.Models.Category;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.GetCategories;

internal class GetCategoriesCommandHandler : IRequestHandler<GetCategoriesCommand, GetCategoriesModel>
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;

    public GetCategoriesCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
    }

    public async Task<GetCategoriesModel> Handle(GetCategoriesCommand request, CancellationToken cancellationToken)
    {
        var gettingCategoriesResult = await categoryRepository.SearchAsync(mapper.Map<CategorySearch>(request));
        gettingCategoriesResult.EnsureSuccess();
        
        return new GetCategoriesModel
        {
            Categories = mapper.Map<ICollection<GetCategoryModel>>(gettingCategoriesResult.Result)
        };
    }
}