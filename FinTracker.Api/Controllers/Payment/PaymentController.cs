using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FinTracker.Api.Controllers.Payment.Dto.Requests;
using FinTracker.Api.Controllers.Payment.Dto.Responses;
using FinTracker.Infra.Utils;
using FinTracker.Logic.Handlers.Payment.CreatePayment;
using FinTracker.Logic.Handlers.Payment.GetPayment;
using FinTracker.Logic.Handlers.Payment.GetPayments;
using FinTracker.Logic.Handlers.Payment.RemovePayment;
using FinTracker.Logic.Handlers.Payment.UpdatePayment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinTracker.Api.Controllers.Payment;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/payments")]
public class PaymentController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IMediator mediator;

    public PaymentController(IMapper mapper, IMediator mediator)
    {
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Добавить платёж
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CreatePaymentResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreatePayment([FromRoute] string version, [FromBody] CreatePaymentRequest createPaymentRequest)
    {
        var createdPayment = await mediator.Send(mapper.Map<CreatePaymentCommand>(createPaymentRequest));
        
        var response = mapper.Map<CreatePaymentResponse>(createdPayment);
        
        return CreatedAtAction(nameof(GetPayment), new { version, id = response.PaymentId }, response);
    }

    /// <summary>
    /// Получить информацию о совершенном платеже
    /// </summary>
    [HttpGet("{id::guid}")]
    [ProducesResponseType(typeof(GetPaymentResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetPayment([FromRoute] Guid id)
    {
        var payment = await mediator.Send(mapper.Map<GetPaymentCommand>(id));
        
        var response = mapper.Map<GetPaymentResponse>(payment);

        return Ok(response);
    }
    
    /// <summary>
    /// Получить платежи
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ItemsResponse<GetPaymentResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetPayments([FromQuery] GetPaymentsRequest request)
    {
        var payments = await mediator.Send(mapper.Map<GetPaymentsCommand>(request));
        var items = new ItemsResponse<GetPaymentResponse>(mapper.Map<ICollection<GetPaymentResponse>>(payments));

        return Ok(items);
    }

    /// <summary>
    /// Изменить информацию о платеже
    /// </summary>
    [HttpPatch("{id::guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdatePayment(Guid id, [FromBody] UpdatePaymentRequest updatePaymentRequest)
    {
        await mediator.Send(mapper.Map<UpdatePaymentCommand>((id, updatePaymentRequest)));
        
        return Ok();
    }

    /// <summary>
    /// Удалить платёж
    /// </summary>
    [HttpDelete("{id::guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> RemovePayment(Guid id)
    {
        await mediator.Send(mapper.Map<RemovePaymentCommand>(id));

        return Ok();
    }
}