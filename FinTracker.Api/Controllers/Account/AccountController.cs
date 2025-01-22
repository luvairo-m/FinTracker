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
    public async Task<IActionResult> CreateAccountAsync([FromRoute] string version, [FromBody] CreateAccountRequest createAccountRequest)
    {
        var createdAccount = await mediator.Send(mapper.Map<CreateAccountCommand>(createAccountRequest));

        var response = mapper.Map<CreateAccountResponse>(createdAccount);
        
        return CreatedAtAction(nameof(GetAccount), new { version, id = response.AccountId }, response);
    }

    /// <summary>
    /// Получить счёт по идентификатору
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType<GetAccountResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAccount([FromRoute] Guid id)
    {
        var account = await mediator.Send(mapper.Map<GetAccountCommand>(id));
        
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
    [HttpPatch("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateAccountAsync([FromRoute] Guid id, [FromBody] UpdateAccountRequest updateAccountRequest)
    {
        await mediator.Send(mapper.Map<UpdateAccountCommand>((id, updateAccountRequest)));
        
        return Ok();
    }

    /// <summary>
    /// Удалить счёт
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> RemoveAccountAsync([FromRoute] Guid id)
    {
        await mediator.Send(mapper.Map<RemoveAccountCommand>(id));
        
        return Ok();
    }
}