using AutoMapper;
using FinTracker.Dal.Models.Categories;
using FinTracker.Logic.Handlers.Category.CreateCategory;
using FinTracker.Logic.Handlers.Category.GetCategories;
using FinTracker.Logic.Handlers.Category.GetCategory;
using FinTracker.Logic.Handlers.Category.UpdateCategory;
using FinTracker.Logic.Models.Category;

namespace FinTracker.Logic.Mappers.Category;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<CreateCategoryCommand, Dal.Models.Categories.Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<GetCategoriesCommand, CategorySearch>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TitleSubstring, opt => opt.MapFrom(src => src.TitleSubstring));

        CreateMap<GetCategoryCommand, CategorySearch>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.TitleSubstring, opt => opt.Ignore());
        
        CreateMap<Dal.Models.Categories.Category, GetCategoryModel>();
        
        CreateMap<UpdateCategoryCommand, Dal.Models.Categories.Category>();
    }
}