using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FinTracker.Api.Controllers.Bill.Dto.Requests;
using FinTracker.Api.Controllers.Bill.Dto.Responses;
using FinTracker.Logic.Handlers.Bill.CreateBill;
using FinTracker.Logic.Handlers.Bill.DeleteBill;
using FinTracker.Logic.Handlers.Bill.GetBill;
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
    public async Task<IActionResult> CreateBill([FromBody] CreateBillRequest createBillRequest)
    {
        var createdBill = await mediator.Send(new CreateBillCommand(
            title: createBillRequest.Title,
            balance: createBillRequest.Balance,
            description: createBillRequest.Description,
            currencyId: createBillRequest.CurrencyId));
        
        var response = mapper.Map<CreateBillResponse>(createdBill);
        
        return Ok(response);
    }

    /// <summary>
    /// Получить счёт по идентификатору
    /// </summary>
    [HttpGet("{billId:guid}")]
    [ProducesResponseType<GetBillResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBill([FromRoute] Guid billId)
    {
        var bill = await mediator.Send(new GetBillCommand(billId: billId));
        
        var response = mapper.Map<GetBillResponse>(bill);
        
        return Ok(response);
    }

    /// <summary>
    /// Получить счет
    /// </summary>
    [HttpGet]
    [ProducesResponseType<GetBillsResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBills()
    {
        var bills = await mediator.Send(new GetBillsCommand());
        
        var response = mapper.Map<GetBillsResponse>(bills);
        
        return Ok(response);
    }

    /// <summary>
    /// Обновить данные счёта
    /// </summary>
    [HttpPut("{billId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateBill([FromRoute] Guid billId, [FromBody] UpdateBillRequest updateBillRequest)
    {
        await mediator.Send(new UpdateBillCommand(
            id: billId,
            title: updateBillRequest.Title,
            balance: updateBillRequest.Balance,
            description: updateBillRequest.Description,
            currencyId: updateBillRequest.CurrencyId));
        
        return Ok();
    }

    /// <summary>
    /// Удалить счёт
    /// </summary>
    [HttpDelete("{billId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteBill([FromRoute] Guid billId)
    {
        await mediator.Send(new DeleteBillCommand(billId: billId));
        
        return Ok();
    }
}