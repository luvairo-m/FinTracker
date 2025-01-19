using AutoMapper;
using FinTracker.Dal.Models.Accounts;
using FinTracker.Dal.Models.Payments;
using FinTracker.Logic.Handlers.Account.CreateAccount;
using FinTracker.Logic.Handlers.Account.GetAccount;
using FinTracker.Logic.Handlers.Account.GetAccounts;
using FinTracker.Logic.Handlers.Account.UpdateAccount;
using FinTracker.Logic.Models.Account;

namespace FinTracker.Logic.Mappers.Account;

public class AccountMapper : Profile
{
    public AccountMapper()
    {
        CreateMap<GetAccountCommand, AccountSearch>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AccountId))
            .ForMember(dest => dest.TitleSubstring, opt => opt.Ignore())
            .ForMember(dest => dest.CurrencyId, opt => opt.Ignore());

        CreateMap<GetAccountsCommand, AccountSearch>();

        CreateMap<UpdateAccountCommand, PaymentSearch>()
            .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TitleSubstring, opt => opt.Ignore())
            .ForMember(dest => dest.MinAmount, opt => opt.Ignore())
            .ForMember(dest => dest.MaxAmount, opt => opt.Ignore())
            .ForMember(dest => dest.Types, opt => opt.Ignore())
            .ForMember(dest => dest.MinDate, opt => opt.Ignore())
            .ForMember(dest => dest.MaxDate, opt => opt.Ignore())
            .ForMember(dest => dest.Months, opt => opt.Ignore())
            .ForMember(dest => dest.Years, opt => opt.Ignore())
            .ForMember(dest => dest.Categories, opt => opt.Ignore()); 
        
        CreateMap<CreateAccountCommand, Dal.Models.Accounts.Account>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        
        CreateMap<Dal.Models.Accounts.Account, GetAccountModel>();

        CreateMap<UpdateAccountCommand, Dal.Models.Accounts.Account>()
            .ForMember(dest => dest.Balance, opt => opt.Ignore());
    }
}