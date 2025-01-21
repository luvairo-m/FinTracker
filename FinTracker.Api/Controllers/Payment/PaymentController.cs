﻿using System;
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
    [ProducesResponseType<CreatePaymentResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest createPaymentRequest)
    {
        var createdPayment = await mediator.Send(mapper.Map<CreatePaymentCommand>(createPaymentRequest));
        
        var response = mapper.Map<CreatePaymentResponse>(createdPayment);
        
        return Ok(response);
    }

    /// <summary>
    /// Получить информацию о совершенном платеже
    /// </summary>
    [HttpGet("{paymentId::guid}")]
    [ProducesResponseType<GetPaymentResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetPayment([FromRoute] Guid paymentId)
    {
        var payment = await mediator.Send(mapper.Map<GetPaymentCommand>(paymentId));
        
        var response = mapper.Map<GetPaymentResponse>(payment);

        return Ok(response);
    }
    
    /// <summary>
    /// Получить платежи
    /// </summary>
    [HttpGet]
    [ProducesResponseType<ItemsResponse<GetPaymentResponse>>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetPayments([FromQuery] GetPaymentsRequest request)
    {
        var payments = await mediator.Send(mapper.Map<GetPaymentsCommand>(request));
        var items = new ItemsResponse<GetPaymentResponse>(mapper.Map<ICollection<GetPaymentResponse>>(payments));

        return Ok(items);
    }

    /// <summary>
    /// Изменить информацию о платеже
    /// </summary>
    [HttpPut("{paymentId::guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdatePayment(Guid paymentId, [FromBody] UpdatePaymentRequest updatePaymentRequest)
    {
        await mediator.Send(mapper.Map<UpdatePaymentCommand>((paymentId, updatePaymentRequest)));
        
        return Ok();
    }

    /// <summary>
    /// Удалить платёж
    /// </summary>
    [HttpDelete("{paymentId::guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> RemovePayment(Guid paymentId)
    {
        await mediator.Send(mapper.Map<RemovePaymentCommand>(paymentId));

        return Ok();
    }
}