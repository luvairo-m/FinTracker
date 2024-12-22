using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FinTracker.Api.Controllers.Payment.Dto.Requests;
using FinTracker.Api.Controllers.Payment.Dto.Responses;
using FinTracker.Logic.Managers.Payment.Interfaces;
using FinTracker.Logic.Models.Payment.Params;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinTracker.Api.Controllers.Payment;

[ApiController]
[Route("api/payments")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentManager _paymentManager;
    private readonly IMapper _mapper;

    public PaymentController(IPaymentManager paymentManager, IMapper mapper)
    {
        _paymentManager = paymentManager;
        _mapper = mapper;
    }

    /// <summary>
    /// Добавить платёж
    /// </summary>
    [HttpPost]
    [ProducesResponseType<CreatePaymentResponse>((int)HttpStatusCode.OK)]
    public async Task<ActionResult> CreatePayment([FromBody] CreatePaymentRequest createPaymentRequest)
    {
        var createPaymentParam = _mapper.Map<CreatePaymentParam>(createPaymentRequest);
        
        var createPaymentResult = await _paymentManager.CreatePayment(createPaymentParam);
        
        var response = _mapper.Map<CreatePaymentResponse>(createPaymentResult);
        
        return Ok(response);
    }

    /// <summary>
    /// Получить информацию о платеже
    /// </summary>
    [HttpGet("{paymentId::guid}")]
    [ProducesResponseType<GetPaymentResponse>((int)HttpStatusCode.OK)]
    public async Task<ActionResult> GetPayment(Guid paymentId)
    {
        var getPaymentResult = await _paymentManager.GetPayment(paymentId);
        
        var response = _mapper.Map<GetPaymentResponse>(getPaymentResult);
        
        return Ok(response);
    }

    /// <summary>
    /// Получить информацию о совершенных платежах
    /// </summary>
    [HttpGet]
    [ProducesResponseType<GetPaymentsResponse>((int)HttpStatusCode.OK)]
    public async Task<ActionResult> GetPayments()
    {
        var getPaymentsResult = await _paymentManager.GetPayments();
        
        var response = _mapper.Map<GetPaymentsResponse>(getPaymentsResult);

        return Ok(response);
    }

    /// <summary>
    /// Изменить информацию о платеже
    /// </summary>
    [HttpPut("{paymentId::guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> UpdatePayment(Guid paymentId, [FromBody] UpdatePaymentRequest updatePaymentRequest)
    {
        var updatePaymentParam = _mapper.Map<UpdatePaymentParam>((paymentId, updatePaymentRequest));
        
        await _paymentManager.UpdatePayment(updatePaymentParam);
        
        return Ok();
    }

    /// <summary>
    /// Удалить платёж
    /// </summary>
    [HttpDelete("{paymentId::guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> DeletePayment(Guid paymentId)
    {
        await _paymentManager.DeletePayment(paymentId);

        return Ok();
    }
}