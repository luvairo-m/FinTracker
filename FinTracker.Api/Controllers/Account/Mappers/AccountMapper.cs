﻿using System;
using AutoMapper;
using FinTracker.Api.Controllers.Account.Dto.Requests;
using FinTracker.Api.Controllers.Account.Dto.Responses;
using FinTracker.Api.Controllers.Bill.Dto.Responses;
using FinTracker.Logic.Handlers.Account.CreateAccount;
using FinTracker.Logic.Handlers.Account.GetAccount;
using FinTracker.Logic.Handlers.Account.GetAccounts;
using FinTracker.Logic.Handlers.Account.RemoveAccount;
using FinTracker.Logic.Handlers.Account.UpdateAccount;
using FinTracker.Logic.Models.Account;

namespace FinTracker.Api.Controllers.Account.Mappers;

public class AccountMapper : Profile
{
    public AccountMapper()
    {
        CreateMap<CreateAccountRequest, CreateAccountCommand>();
        
        CreateMap<CreateAccountModel, CreateAccountResponse>();
        
        CreateMap<Guid, GetAccountCommand>()
            .ForMember(dest => dest.BillId, y => y.MapFrom(z => z));
        
        CreateMap<GetAccountModel, GetAccountResponse>();
        
        CreateMap<GetAccountsRequest, GetAccountsCommand>();

        CreateMap<(Guid accountId, UpdateAccountRequest updateAccountRequest), UpdateAccountCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.accountId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.updateAccountRequest.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.updateAccountRequest.Description))
            .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.updateAccountRequest.CurrencyId));
        
        CreateMap<Guid, RemoveAccountCommand>()
            .ForMember(dest => dest.AccountId, y => y.MapFrom(z => z));
    }
}