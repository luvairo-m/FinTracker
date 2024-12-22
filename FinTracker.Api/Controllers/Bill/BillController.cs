using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FinTracker.Api.Controllers.Bill.Dto.Requests;
using FinTracker.Api.Controllers.Bill.Dto.Responses;
using FinTracker.Logic.Managers.Bill.Interfaces;
using FinTracker.Logic.Models.Bill.Params;
using Microsoft.AspNetCore.Mvc;

namespace FinTracker.Api.Controllers.Bill;

[ApiController]
[Route("api/bills")]
public class BillController : ControllerBase
{
    private readonly IBillManager _billManager;
    private readonly IMapper _mapper;

    public BillController(IBillManager billManager, IMapper mapper)
    {
        _billManager = billManager;
        _mapper = mapper;
    }

    /// <summary>
    /// Создать счёт
    /// </summary>
    [HttpPost]
    [ProducesResponseType<CreateBillResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateBill([FromBody] CreateBillRequest createBillRequest)
    {
        var createBillParam = _mapper.Map<CreateBillParam>(createBillRequest);
        
        var createBillResult = await _billManager.CreateBill(createBillParam);
        
        var response = _mapper.Map<CreateBillResponse>(createBillResult);
        
        return Ok(response);
    }

    /// <summary>
    /// Получить счёт по идентификатору
    /// </summary>
    [HttpGet("{billId:guid}")]
    [ProducesResponseType<GetBillResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBill([FromRoute] Guid billId)
    {
        var getBillResult = await _billManager.GetBill(billId);
        
        var response = _mapper.Map<GetBillResponse>(getBillResult);
        
        return Ok(response);
    }

    /// <summary>
    /// Получить счет
    /// </summary>
    [HttpGet]
    [ProducesResponseType<GetBillsResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBills()
    {
        var getBillsResult = await _billManager.GetBills();
        
        var response = _mapper.Map<GetBillsResponse>(getBillsResult);
        
        return Ok(response);
    }

    /// <summary>
    /// Обновить данные счёта
    /// </summary>
    [HttpPut("{billId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateBill([FromRoute] Guid billId, [FromBody] UpdateBillRequest updateBillRequest)
    {
        var updateBillParam = _mapper.Map<UpdateBillParam>((billId, updateBillRequest));
        
        await _billManager.UpdateBill(updateBillParam);

        return Ok();
    }

    /// <summary>
    /// Удалить счёт
    /// </summary>
    [HttpDelete("{billId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteBill([FromRoute] Guid billId)
    {
        await _billManager.DeleteBill(billId);
        
        return Ok();
    }

}