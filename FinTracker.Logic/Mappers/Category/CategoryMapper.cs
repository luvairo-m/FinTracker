using AutoMapper;
using FinTracker.Logic.Models.Category;

namespace FinTracker.Logic.Mappers.Category;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<Dal.Models.Categories.Category, GetCategoryModel>();

        CreateMap<ICollection<Dal.Models.Categories.Category>, ICollection<GetCategoryModel>>();
    }
}