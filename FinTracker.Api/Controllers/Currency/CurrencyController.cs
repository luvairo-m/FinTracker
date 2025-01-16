using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FinTracker.Api.Controllers.Currency.Dto.Requests;
using FinTracker.Api.Controllers.Currency.Dto.Responses;
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
    public async Task<IActionResult> CreateCurrency([FromBody] CreateCurrencyRequest createCurrencyRequest)
    {
        var createdCurrency = await mediator.Send(mapper.Map<CreateCurrencyCommand>(createCurrencyRequest));

        var response = mapper.Map<CreateCurrencyResponse>(createdCurrency);

        return Ok(response);
    }

    /// <summary>
    /// Получить информацию о валюте
    /// </summary>
    [HttpGet("{currencyId:guid}")]
    [ProducesResponseType<GetCurrencyResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCurrency(Guid currencyId)
    {
        var currency = await mediator.Send(mapper.Map<GetCurrencyCommand>(currencyId));

        var response = mapper.Map<GetCurrencyResponse>(currency);

        return Ok(response);
    }

    /// <summary>
    /// Получить список валют
    /// </summary>
    [HttpGet]
    [ProducesResponseType<GetCurrenciesResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCurrencies([FromQuery] GetCurrenciesRequest getCurrenciesRequest)
    {
        var currencies = await mediator.Send(mapper.Map<GetCurrenciesCommand>(getCurrenciesRequest));

        var response = mapper.Map<GetCurrenciesResponse>(currencies);

        return Ok(response);
    }

    /// <summary>
    /// Обновить информацию о валюте
    /// </summary>
    [HttpPut("{currencyId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateCurrency(Guid currencyId, [FromBody] UpdateCurrencyRequest updateCurrencyRequest)
    {
        await mediator.Send(mapper.Map<UpdateCurrencyCommand>((currencyId, updateCurrencyRequest)));

        return Ok();
    }

    /// <summary>
    /// Удалить валюту
    /// </summary>
    [HttpDelete("{currencyId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> RemoveCurrency(Guid currencyId)
    {
        await mediator.Send(mapper.Map<RemoveCurrencyCommand>(currencyId));

        return Ok();
    }
}
