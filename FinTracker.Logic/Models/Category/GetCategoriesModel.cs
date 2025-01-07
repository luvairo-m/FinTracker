namespace FinTracker.Logic.Models.Category;

public struct GetCategoriesModel
{
    public ICollection<GetCategoryModel> Categories { get; set; }
}