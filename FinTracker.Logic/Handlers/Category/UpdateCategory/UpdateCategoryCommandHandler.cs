using AutoMapper;
using FinTracker.Dal.Models.Categories;
using FinTracker.Dal.Repositories.Categories;
using MediatR;

namespace FinTracker.Logic.Handlers.Category.UpdateCategory;

internal class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly CategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(CategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var searchResult = await _categoryRepository.SearchAsync(new CategorySearch { Id = request.CategoryId });
        
        var existingCategory = searchResult.Result.FirstOrDefault();

        if (existingCategory is not null)
        {
            var updatedCategory = _mapper.Map(request, existingCategory);

            await _categoryRepository.UpdateAsync(updatedCategory);
        }
    }
}