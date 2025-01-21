using AutoMapper;
using FinTracker.Dal.Models.Accounts;
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
        CreateMap<GetAccountCommand, AccountSearch>(MemberList.Destination)
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AccountId))
            .ForMember(dest => dest.TitleSubstring, opt => opt.Ignore())
            .ForMember(dest => dest.CurrencyId, opt => opt.Ignore());

        CreateMap<GetAccountsCommand, AccountSearch>(MemberList.Destination);

        CreateMap<CreateAccountCommand, Dal.Models.Accounts.Account>(MemberList.Destination)
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        
        CreateMap<Dal.Models.Accounts.Account, GetAccountModel>(MemberList.Destination);

        CreateMap<UpdateAccountCommand, Dal.Models.Accounts.Account>(MemberList.Destination);
    }
}