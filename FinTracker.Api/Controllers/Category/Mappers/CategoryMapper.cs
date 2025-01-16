using System;
using AutoMapper;
using FinTracker.Api.Controllers.Category.Dto.Requests;
using FinTracker.Api.Controllers.Category.Dto.Responses;
using FinTracker.Logic.Handlers.Category.CreateCategory;
using FinTracker.Logic.Handlers.Category.GetCategories;
using FinTracker.Logic.Handlers.Category.GetCategory;
using FinTracker.Logic.Handlers.Category.RemoveCategory;
using FinTracker.Logic.Handlers.Category.UpdateCategory;
using FinTracker.Logic.Models.Category;

namespace FinTracker.Api.Controllers.Category.Mappers;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<CreateCategoryRequest, CreateCategoryCommand>();
        
        CreateMap<CreateCategoryModel, CreateCategoryResponse>();

        CreateMap<Guid, GetCategoryCommand>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src));

        CreateMap<GetCategoryModel, GetCategoryResponse>();

        CreateMap<GetCategoriesRequest, GetCategoriesCommand>();
        
        CreateMap<GetCategoriesModel, GetCategoriesResponse>();
        
        CreateMap<(Guid categoryId, UpdateCategoryRequest updateAccountRequest), UpdateCategoryCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.categoryId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.updateAccountRequest.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.updateAccountRequest.Description));

        CreateMap<Guid, RemoveCategoryCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));
    }
}