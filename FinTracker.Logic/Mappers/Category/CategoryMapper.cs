using AutoMapper;
using FinTracker.Logic.Handlers.Category.UpdateCategory;
using FinTracker.Logic.Models.Category;

namespace FinTracker.Logic.Mappers.Category;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<Dal.Models.Categories.Category, GetCategoryModel>();
        
        CreateMap<UpdateCategoryCommand, Dal.Models.Categories.Category>();
    }
}