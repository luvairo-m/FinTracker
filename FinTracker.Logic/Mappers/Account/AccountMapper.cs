using AutoMapper;
using FinTracker.Dal.Models.Accounts;
using FinTracker.Logic.Handlers.Account.CreateAccount;
using FinTracker.Logic.Handlers.Account.GetAccount;
using FinTracker.Logic.Handlers.Account.GetAccounts;
using FinTracker.Logic.Handlers.Account.UpdateAccount;
using FinTracker.Logic.Models.Account;

namespace FinTracker.Logic.Mappers.Account;

// ReSharper disable once UnusedType.Global
public class AccountMapper : Profile
{
    public AccountMapper()
    {
        CreateMap<GetAccountCommand, AccountSearch>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AccountId))
            .ForMember(dest => dest.TitleSubstring, opt => opt.Ignore())
            .ForMember(dest => dest.CurrencyId, opt => opt.Ignore());

        CreateMap<GetAccountsCommand, AccountSearch>()
            .ForMember(dest => dest.Id, src => src.Ignore());

        CreateMap<CreateAccountCommand, Dal.Models.Accounts.Account>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        
        CreateMap<Dal.Models.Accounts.Account, GetAccountModel>();

        CreateMap<UpdateAccountCommand, Dal.Models.Accounts.Account>();
    }
}