using AutoMapper;
using FinTracker.Dal.Logic;
using FinTracker.Dal.Models.Categories;
using FinTracker.Dal.Repositories.Categories;
using FinTracker.Logic.Models.Category;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.GetCategories;

internal class GetCategoriesCommandHandler : IRequestHandler<GetCategoriesCommand, ICollection<GetCategoryModel>>
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;

    public GetCategoriesCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
    }

    public async Task<ICollection<GetCategoryModel>> Handle(GetCategoriesCommand request, CancellationToken cancellationToken)
    {
        var getResult = await categoryRepository.SearchAsync(mapper.Map<CategorySearch>(request));

        if (getResult.Status == DbQueryResultStatus.NotFound)
        {
            return Array.Empty<GetCategoryModel>();
        }
        
        getResult.EnsureSuccess();

        return mapper.Map<ICollection<GetCategoryModel>>(getResult.Result);
    }
}