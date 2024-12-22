using FinTracker.Logic.Managers.Category.Interfaces;
using FinTracker.Logic.Models.Category.Params;
using FinTracker.Logic.Models.Category.Results;

namespace FinTracker.Logic.Managers.Category;

public class CategoryManager : ICategoryManager
{
    public Task<CreateCategoryResult> CreateCategory(CreateCategoryParam createCategoryParam)
    {
        throw new NotImplementedException();
    }

    public Task<GetCategoryResult> GetCategory(Guid categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<GetCategoriesResult> GetCategories()
    {
        throw new NotImplementedException();
    }

    public Task UpdateCategory(UpdateCategoryParam updateCategoryParam)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCategory(Guid categoryId)
    {
        throw new NotImplementedException();
    }
}