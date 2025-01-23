using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FinTracker.Api.Controllers.Category.Dto.Requests;
using FinTracker.Api.Controllers.Category.Dto.Responses;
using FinTracker.Api.Models;
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
    [ProducesResponseType(typeof(CreateCategoryResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateCategory([FromRoute] string version, [FromBody] CreateCategoryRequest createCategoryRequest)
    {
        var createdCategory = await mediator.Send(mapper.Map<CreateCategoryCommand>(createCategoryRequest));
        
        var response = mapper.Map<CreateCategoryResponse>(createdCategory);

        return CreatedAtAction(nameof(GetCategory), new { version, id = response.CategoryId }, response);
    }

    /// <summary>
    /// Получить информацию о категории
    /// </summary>
    [HttpGet("{id::guid}")]
    [ProducesResponseType(typeof(GetCategoryResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCategory(Guid id)
    {
        var category = await mediator.Send(mapper.Map<GetCategoryCommand>(id));
        
        var response = mapper.Map<GetCategoryResponse>(category);
        
        return Ok(response);
    }

    /// <summary>
    /// Получить категории
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ItemsResponse<GetCategoryResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCategories([FromQuery] GetCategoriesRequest getCategoriesRequest)
    {
        var categories = await mediator.Send(mapper.Map<GetCategoriesCommand>(getCategoriesRequest));
        var items = new ItemsResponse<GetCategoryResponse>(mapper.Map<ICollection<GetCategoryResponse>>(categories));
        
        return Ok(items);
    }

    /// <summary>
    /// Изменить информацию о категории
    /// </summary>
    [HttpPatch("{id::guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryRequest updateCategoryRequest)
    {
        await mediator.Send(mapper.Map<UpdateCategoryCommand>((id, updateCategoryRequest)));
        
        return Ok();
    }

    /// <summary>
    /// Удалить категорию
    /// </summary>
    [HttpDelete("{id::guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> RemoveCategory(Guid id)
    {
        await mediator.Send(mapper.Map<RemoveCategoryCommand>(id));
        
        return Ok();
    }
}