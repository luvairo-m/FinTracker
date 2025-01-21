using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FinTracker.Api.Controllers.Category.Dto.Requests;
using FinTracker.Api.Controllers.Category.Dto.Responses;
using FinTracker.Infra.Utils;
using FinTracker.Logic.Handlers.Category.CreateCategory;
using FinTracker.Logic.Handlers.Category.GetCategories;
using FinTracker.Logic.Handlers.Category.GetCategory;
using FinTracker.Logic.Handlers.Category.RemoveCategory;
using FinTracker.Logic.Handlers.Category.UpdateCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinTracker.Api.Controllers.Category;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/categories")]
public class CategoryController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IMediator mediator;

    public CategoryController(IMapper mapper, IMediator mediator)
    {
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    /// <summary>
    /// Добавить категорию
    /// </summary>
    [HttpPost]
    [ProducesResponseType<CreateCategoryResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest createCategoryRequest)
    {
        var createdCategory = await mediator.Send(mapper.Map<CreateCategoryCommand>(createCategoryRequest));
        
        var response = mapper.Map<CreateCategoryResponse>(createdCategory);
        
        return Ok(response);
    }

    /// <summary>
    /// Получить информацию о категории
    /// </summary>
    [HttpGet("{categoryId::guid}")]
    [ProducesResponseType<GetCategoryResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCategory(Guid categoryId)
    {
        var category = await mediator.Send(mapper.Map<GetCategoryCommand>(categoryId));
        
        var response = mapper.Map<GetCategoryResponse>(category);
        
        return Ok(response);
    }

    /// <summary>
    /// Получить категории
    /// </summary>
    [HttpGet]
    [ProducesResponseType<ItemsResponse<GetCategoryResponse>>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCategories([FromQuery] GetCategoriesRequest getCategoriesRequest)
    {
        var categories = await mediator.Send(mapper.Map<GetCategoriesCommand>(getCategoriesRequest));
        var items = new ItemsResponse<GetCategoryResponse>(mapper.Map<ICollection<GetCategoryResponse>>(categories));
        
        return Ok(items);
    }

    /// <summary>
    /// Изменить информацию о категории
    /// </summary>
    [HttpPut("{categoryId::guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateCategory(Guid categoryId, [FromBody] UpdateCategoryRequest updateCategoryRequest)
    {
        await mediator.Send(mapper.Map<UpdateCategoryCommand>((categoryId, updateCategoryRequest)));
        
        return Ok();
    }

    /// <summary>
    /// Удалить категорию
    /// </summary>
    [HttpDelete("{categoryId::guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> RemoveCategory(Guid categoryId)
    {
        await mediator.Send(mapper.Map<RemoveCategoryCommand>(categoryId));
        
        return Ok();
    }
}