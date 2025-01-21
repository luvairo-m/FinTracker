using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FinTracker.Api.Controllers.Account.Dto.Requests;
using FinTracker.Api.Controllers.Account.Dto.Responses;
using FinTracker.Infra.Utils;
using FinTracker.Logic.Handlers.Account.CreateAccount;
using FinTracker.Logic.Handlers.Account.GetAccount;
using FinTracker.Logic.Handlers.Account.GetAccounts;
using FinTracker.Logic.Handlers.Account.RemoveAccount;
using FinTracker.Logic.Handlers.Account.UpdateAccount;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinTracker.Api.Controllers.Account;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/accounts")]
public class AccountController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IMediator mediator;

    public AccountController(IMapper mapper, IMediator mediator)
    {
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Создать счёт
    /// </summary>
    [HttpPost]
    [ProducesResponseType<CreateAccountResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateAccountAsync([FromBody] CreateAccountRequest createAccountRequest)
    {
        var createdAccount = await mediator.Send(mapper.Map<CreateAccountCommand>(createAccountRequest));
        
        return Ok(mapper.Map<CreateAccountResponse>(createdAccount));
    }

    /// <summary>
    /// Получить счёт по идентификатору
    /// </summary>
    [HttpGet("{accountId:guid}")]
    [ProducesResponseType<GetAccountResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAccount([FromRoute] Guid accountId)
    {
        var account = await mediator.Send(mapper.Map<GetAccountCommand>(accountId));
        
        var response = mapper.Map<GetAccountResponse>(account);
        
        return Ok(response);
    }
    
    /// <summary>
    /// Получить счета
    /// </summary>
    [HttpGet]
    [ProducesResponseType<ItemsResponse<GetAccountResponse>>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAccountsAsync([FromQuery] GetAccountsRequest request)
    {
        var accounts = await mediator.Send(mapper.Map<GetAccountsCommand>(request));
        var items = new ItemsResponse<GetAccountResponse>(mapper.Map<ICollection<GetAccountResponse>>(accounts));

        return Ok(items);
    }

    /// <summary>
    /// Обновить данные счёта
    /// </summary>
    [HttpPut("{accountId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateAccountAsync([FromRoute] Guid accountId, [FromBody] UpdateAccountRequest updateAccountRequest)
    {
        await mediator.Send(mapper.Map<UpdateAccountCommand>((accountId, updateAccountRequest)));
        
        return Ok();
    }

    /// <summary>
    /// Удалить счёт
    /// </summary>
    [HttpDelete("{accountId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> RemoveAccountAsync([FromRoute] Guid accountId)
    {
        await mediator.Send(mapper.Map<RemoveAccountCommand>(accountId));
        
        return Ok();
    }
}