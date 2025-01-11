using AutoMapper;
using FinTracker.Dal.Repositories.Categories;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.UpdateCategory;

internal class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
    }

    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = mapper.Map<Dal.Models.Categories.Category>(request);

        var updatingCategoryResult = await categoryRepository.UpdateAsync(category);
        updatingCategoryResult.EnsureSuccess();
    }
}