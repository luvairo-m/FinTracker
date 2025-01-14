﻿using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FinTracker.Api.Controllers.Payment.Dto.Requests;
using FinTracker.Api.Controllers.Payment.Dto.Responses;
using FinTracker.Logic.Handlers.Payment.CreatePayment;
using FinTracker.Logic.Handlers.Payment.DeletePayment;
using FinTracker.Logic.Handlers.Payment.GetPayments;
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
        var createdPayment = await mediator.Send(new CreatePaymentCommand(
            title: createPaymentRequest.Title,
            description: createPaymentRequest.Description,
            amount: createPaymentRequest.Amount,
            billId: createPaymentRequest.BillId,
            categoryId:createPaymentRequest.CategoryId,
            type: createPaymentRequest.Type));
        
        var response = mapper.Map<CreatePaymentResponse>(createdPayment);
        
        return Ok(response);
    }

    /// <summary>
    /// Получить информацию о совершенных платежах
    /// </summary>
    [HttpGet]
    [ProducesResponseType<GetPaymentsResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetPayments([FromQuery] GetPaymentsRequest request)
    {
        var payments = await mediator.Send(new GetPaymentsCommand(
            id: request.Id,
            minAmount: request.MinAmount,
            maxAmount: request.MaxAmount,
            types: request.Types,
            minDate: request.MinDate,
            maxDate: request.MaxDate,
            months: request.Months,
            years: request.Years,
            billId: request.BillId));
        
        var response = mapper.Map<GetPaymentsResponse>(payments);

        return Ok(response);
    }

    /// <summary>
    /// Изменить информацию о платеже
    /// </summary>
    [HttpPut("{paymentId::guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdatePayment(Guid paymentId, [FromBody] UpdatePaymentRequest updatePaymentRequest)
    {
        await mediator.Send(new UpdatePaymentCommand(
            id: paymentId,
            title: updatePaymentRequest.Title,
            description: updatePaymentRequest.Description,
            amount: updatePaymentRequest.Amount,
            billId: updatePaymentRequest.BillId,
            type: updatePaymentRequest.Type));
        
        return Ok();
    }

    /// <summary>
    /// Удалить платёж
    /// </summary>
    [HttpDelete("{paymentId::guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeletePayment(Guid paymentId)
    {
        await mediator.Send(new DeletePaymentCommand(paymentId: paymentId));

        return Ok();
    }
}