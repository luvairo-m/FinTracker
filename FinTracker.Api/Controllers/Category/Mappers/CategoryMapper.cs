using System;
using AutoMapper;
using FinTracker.Api.Controllers.Category.Dto.Requests;
using FinTracker.Api.Controllers.Category.Dto.Responses;
using FinTracker.Logic.Models.Category.Params;
using FinTracker.Logic.Models.Category.Results;

namespace FinTracker.Api.Controllers.Category.Mappers;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<CreateCategoryRequest, CreateCategoryParam>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));
        
        CreateMap<CreateCategoryResult, CreateCategoryResponse>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));
        
        CreateMap<GetCategoryResult, GetCategoryResponse>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));
        
        CreateMap<GetCategoriesResult, GetCategoriesResponse>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories));
        
        CreateMap<(Guid, UpdateCategoryRequest), UpdateCategoryParam>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Item1))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Item2.Title));
    }
}