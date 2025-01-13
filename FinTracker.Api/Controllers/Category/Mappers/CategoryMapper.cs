using AutoMapper;
using FinTracker.Api.Controllers.Category.Dto.Responses;
using FinTracker.Logic.Handlers.Category.UpdateCategory;
using FinTracker.Logic.Models.Category;

namespace FinTracker.Api.Controllers.Category.Mappers;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<CreateCategoryModel, CreateCategoryResponse>();

        CreateMap<GetCategoryModel, GetCategoryResponse>();

        CreateMap<GetCategoriesModel, GetCategoriesResponse>();

        CreateMap<Dal.Models.Categories.Category, GetCategoryModel>();

        CreateMap<UpdateCategoryCommand, Dal.Models.Categories.Category>();
    }
}