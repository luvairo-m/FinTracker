using FinTracker.Logic.Models.Category.Params;
using FinTracker.Logic.Models.Category.Results;

namespace FinTracker.Logic.Managers.Category.Interfaces;

public interface ICategoryManager
{
    Task<CreateCategoryResult> CreateCategory(CreateCategoryParam createCategoryParam);
    
    Task<GetCategoryResult> GetCategory(Guid categoryId);
    
    Task<GetCategoriesResult> GetCategories();
    
    Task UpdateCategory(UpdateCategoryParam updateCategoryParam);
    
    Task DeleteCategory(Guid categoryId);
}