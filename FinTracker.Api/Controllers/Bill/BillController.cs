using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FinTracker.Api.Controllers.Bill.Dto.Requests;
using FinTracker.Api.Controllers.Bill.Dto.Responses;
using FinTracker.Logic.Handlers.Bill.CreateBill;
using FinTracker.Logic.Handlers.Bill.DeleteBill;
using FinTracker.Logic.Handlers.Bill.GetBills;
using FinTracker.Logic.Handlers.Bill.UpdateBill;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinTracker.Api.Controllers.Bill;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/bills")]
public class BillController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IMediator mediator;

    public BillController(IMapper mapper, IMediator mediator)
    {
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Создать счёт
    /// </summary>
    [HttpPost]
    [ProducesResponseType<CreateBillResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateBillAsync([FromBody] CreateBillRequest createBillRequest)
    {
        var createdBill = await mediator.Send(new CreateBillCommand(
            title: createBillRequest.Title,
            balance: createBillRequest.Balance,
            description: createBillRequest.Description,
            currencyId: createBillRequest.CurrencyId));
        
        return Ok(mapper.Map<CreateBillResponse>(createdBill));
    }

    /// <summary>
    /// Получить счета по параметрам
    /// </summary>
    [HttpGet]
    [ProducesResponseType<ICollection<GetBillResponse>>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBillsAsync([FromQuery] GetBillsRequest request)
    {
        var bills = await mediator.Send(mapper.Map<GetBillsRequest, GetBillsCommand>(request));
        
        return Ok(mapper.Map<ICollection<GetBillResponse>>(bills));
    }

    /// <summary>
    /// Обновить данные счёта
    /// </summary>
    [HttpPut("{billId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateBillAsync([FromRoute] Guid billId, [FromBody] UpdateBillRequest updateBillRequest)
    {
        await mediator.Send(new UpdateBillCommand(
            id: billId,
            title: updateBillRequest.Title,
            description: updateBillRequest.Description,
            currencyId: updateBillRequest.CurrencyId));
        
        return Ok();
    }

    /// <summary>
    /// Удалить счёт
    /// </summary>
    [HttpDelete("{billId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteBillAsync([FromRoute] Guid billId)
    {
        await mediator.Send(new DeleteBillCommand(billId: billId));
        
        return Ok();
    }
}