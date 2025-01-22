using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FinTracker.Api.Controllers.Currency.Dto.Requests;
using FinTracker.Api.Controllers.Currency.Dto.Responses;
using FinTracker.Infra.Utils;
using FinTracker.Logic.Handlers.Currency.CreateCurrency;
using FinTracker.Logic.Handlers.Currency.GetCurrencies;
using FinTracker.Logic.Handlers.Currency.GetCurrency;
using FinTracker.Logic.Handlers.Currency.RemoveCurrency;
using FinTracker.Logic.Handlers.Currency.UpdateCurrency;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinTracker.Api.Controllers.Currency;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/currencies")]
public class CurrencyController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IMediator mediator;

    public CurrencyController(IMapper mapper, IMediator mediator)
    {
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Добавить валюту
    /// </summary>
    [HttpPost]
    [ProducesResponseType<CreateCurrencyResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateCurrency([FromRoute] string version, [FromBody] CreateCurrencyRequest createCurrencyRequest)
    {
        var createdCurrency = await mediator.Send(mapper.Map<CreateCurrencyCommand>(createCurrencyRequest));

        var response = mapper.Map<CreateCurrencyResponse>(createdCurrency);

        return CreatedAtAction(nameof(GetCurrency), new { version, id = response.CurrencyId }, response);
    }

    /// <summary>
    /// Получить информацию о валюте
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType<GetCurrencyResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCurrency(Guid id)
    {
        var currency = await mediator.Send(mapper.Map<GetCurrencyCommand>(id));

        var response = mapper.Map<GetCurrencyResponse>(currency);

        return Ok(response);
    }

    /// <summary>
    /// Получить список валют
    /// </summary>
    [HttpGet]
    [ProducesResponseType<ItemsResponse<GetCurrencyResponse>>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCurrencies([FromQuery] GetCurrenciesRequest getCurrenciesRequest)
    {
        var currencies = await mediator.Send(mapper.Map<GetCurrenciesCommand>(getCurrenciesRequest));
        var items = new ItemsResponse<GetCurrencyResponse>(mapper.Map<ICollection<GetCurrencyResponse>>(currencies));

        return Ok(items);
    }

    /// <summary>
    /// Обновить информацию о валюте
    /// </summary>
    [HttpPatch("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateCurrency(Guid id, [FromBody] UpdateCurrencyRequest updateCurrencyRequest)
    {
        await mediator.Send(mapper.Map<UpdateCurrencyCommand>((id, updateCurrencyRequest)));

        return Ok();
    }

    /// <summary>
    /// Удалить валюту
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> RemoveCurrency(Guid id)
    {
        await mediator.Send(mapper.Map<RemoveCurrencyCommand>(id));

        return Ok();
    }
}
