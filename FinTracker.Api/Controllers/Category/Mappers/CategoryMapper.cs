using AutoMapper;
using FinTracker.Api.Controllers.Category.Dto.Responses;
using FinTracker.Logic.Models.Category;

namespace FinTracker.Api.Controllers.Category.Mappers;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<CreateCategoryModel, CreateCategoryResponse>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));
        
        CreateMap<GetCategoryModel, GetCategoryResponse>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));
        
        CreateMap<GetCategoriesModel, GetCategoriesResponse>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories));
    }
}