using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FinTracker.Api.Controllers.Category.Dto.Requests;
using FinTracker.Api.Controllers.Category.Dto.Responses;
using FinTracker.Logic.Handlers.Category.CreateCategory;
using FinTracker.Logic.Handlers.Category.DeleteCategory;
using FinTracker.Logic.Handlers.Category.GetCategories;
using FinTracker.Logic.Handlers.Category.GetCategory;
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
    public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryRequest createCategoryRequest)
    {
        var createdCategory = await mediator.Send(new CreateCategoryCommand(title: createCategoryRequest.Title));
        
        var response = mapper.Map<CreateCategoryResponse>(createdCategory);
        
        return Ok(response);
    }

    /// <summary>
    /// Получить информацию о категории
    /// </summary>
    [HttpGet("{categoryId::guid}")]
    [ProducesResponseType<GetCategoryResponse>((int)HttpStatusCode.OK)]
    public async Task<ActionResult> GetCategory(Guid categoryId)
    {
        var category = await mediator.Send(new GetCategoryCommand(categoryId: categoryId));
        
        var response = mapper.Map<GetCategoryResponse>(category);
     
        return Ok(response);
    }

    /// <summary>
    /// Получить все категории
    /// </summary>
    [HttpGet]
    [ProducesResponseType<GetCategoriesResponse>((int)HttpStatusCode.OK)]
    public async Task<ActionResult> GetCategories()
    {
        var categories = await mediator.Send(new GetCategoriesCommand());
        
        var response = mapper.Map<GetCategoriesResponse>(categories);
        
        return Ok(response);
    }

    /// <summary>
    /// Изменить информацию о категории
    /// </summary>
    [HttpPut("{categoryId::guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> UpdateCategory(Guid categoryId, [FromBody] UpdateCategoryRequest updateCategoryRequest)
    {
        await mediator.Send(new UpdateCategoryCommand(categoryId: categoryId, title: updateCategoryRequest.Title));
        
        return Ok();
    }

    /// <summary>
    /// Удалить категорию
    /// </summary>
    [HttpDelete("{categoryId::guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> DeleteCategory(Guid categoryId)
    {
        await mediator.Send(new DeleteCategoryCommand(categoryId: categoryId));
        
        return Ok();
    }
}