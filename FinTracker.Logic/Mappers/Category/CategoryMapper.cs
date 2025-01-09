using AutoMapper;
using FinTracker.Logic.Handlers.Category.UpdateCategory;
using FinTracker.Logic.Models.Category;

namespace FinTracker.Logic.Mappers.Category;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<Dal.Models.Categories.Category, GetCategoryModel>();
        
        CreateMap<UpdateCategoryCommand, Dal.Models.Categories.Category>()
            .ForMember(dest => dest.Title, opt => opt.Condition(src => src.Title != null))
            .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null));
    }
}