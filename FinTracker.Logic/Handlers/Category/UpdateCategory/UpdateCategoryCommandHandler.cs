using AutoMapper;
using FinTracker.Dal.Models.Categories;
using FinTracker.Dal.Repositories.Categories;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.UpdateCategory;

internal class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Dal.Models.Categories.Category>(request);

        var updatingCategoryResult = await _categoryRepository.UpdateAsync(category);
        updatingCategoryResult.EnsureSuccess();
    }
}