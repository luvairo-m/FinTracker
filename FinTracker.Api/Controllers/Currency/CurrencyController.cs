using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FinTracker.Api.Controllers.Currency.Dto.Requests;
using FinTracker.Api.Controllers.Currency.Dto.Responses;
using FinTracker.Logic.Handlers.Currency.CreateCurrency;
using FinTracker.Logic.Handlers.Currency.DeleteCurrency;
using FinTracker.Logic.Handlers.Currency.GetCurrencies;
using FinTracker.Logic.Handlers.Currency.GetCurrency;
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
        var createdCurrency = await mediator.Send(new CreateCurrencyCommand(
            title: createCurrencyRequest.Title,
            sign: createCurrencyRequest.Sign));

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
        var currency = await mediator.Send(new GetCurrencyCommand(currencyId: currencyId));

        var response = mapper.Map<GetCurrencyResponse>(currency);

        return Ok(response);
    }

    /// <summary>
    /// Получить список валют
    /// </summary>
    [HttpGet]
    [ProducesResponseType<GetCurrenciesResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCurrencies()
    {
        var currencies = await mediator.Send(new GetCurrenciesCommand());

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
        await mediator.Send(new UpdateCurrencyCommand(
            id: currencyId,
            title: updateCurrencyRequest.Title,
            sign: updateCurrencyRequest.Sign));

        return Ok();
    }

    /// <summary>
    /// Удалить валюту
    /// </summary>
    [HttpDelete("{currencyId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteCurrency(Guid currencyId)
    {
        await mediator.Send(new DeleteCurrencyCommand(currencyId: currencyId));

        return Ok();
    }
}
