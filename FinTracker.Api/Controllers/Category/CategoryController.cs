using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FinTracker.Api.Controllers.Category.Dto.Requests;
using FinTracker.Api.Controllers.Category.Dto.Responses;
using FinTracker.Logic.Managers.Category.Interfaces;
using FinTracker.Logic.Models.Category.Params;
using Microsoft.AspNetCore.Mvc;

namespace FinTracker.Api.Controllers.Category;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryManager _categoryManager;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryManager categoryManager, IMapper mapper)
    {
        _categoryManager = categoryManager;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Добавить категорию
    /// </summary>
    [HttpPost]
    [ProducesResponseType<CreateCategoryResponse>((int)HttpStatusCode.OK)]
    public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryRequest createCategoryRequest)
    {
        var createCategoryParam = _mapper.Map<CreateCategoryParam>(createCategoryRequest);
        
        var createCategoryResult = await _categoryManager.CreateCategory(createCategoryParam);
        
        var response = _mapper.Map<CreateCategoryResponse>(createCategoryResult);
        
        return Ok(response);
    }

    /// <summary>
    /// Получить информацию о категории
    /// </summary>
    [HttpGet("{categoryId::guid}")]
    [ProducesResponseType<GetCategoryResponse>((int)HttpStatusCode.OK)]
    public async Task<ActionResult> GetCategory(Guid categoryId)
    {
        var getCategoryResult = await _categoryManager.GetCategory(categoryId);
        
        var response = _mapper.Map<GetCategoryResponse>(getCategoryResult);
        
        return Ok(response);
    }

    /// <summary>
    /// Получить все категории
    /// </summary>
    [HttpGet]
    [ProducesResponseType<GetCategoriesResponse>((int)HttpStatusCode.OK)]
    public async Task<ActionResult> GetCategories()
    {
        var getCategoriesResult = await _categoryManager.GetCategories();
        
        var response = _mapper.Map<GetCategoriesResponse>(getCategoriesResult);

        return Ok(response);
    }

    /// <summary>
    /// Изменить информацию о категории
    /// </summary>
    [HttpPut("{categoryId::guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> UpdateCategory(Guid categoryId, [FromBody] UpdateCategoryRequest updateCategoryRequest)
    {
        var updateCategoryParam = _mapper.Map<UpdateCategoryParam>((categoryId, updateCategoryRequest));
        
        await _categoryManager.UpdateCategory(updateCategoryParam);
        
        return Ok();
    }

    /// <summary>
    /// Удалить категорию
    /// </summary>
    [HttpDelete("{categoryId::guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> DeleteCategory(Guid categoryId)
    {
        await _categoryManager.DeleteCategory(categoryId);

        return Ok();
    }
}