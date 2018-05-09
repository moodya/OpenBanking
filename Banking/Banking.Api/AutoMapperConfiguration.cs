using System;
using AutoMapper;
using Banking.Api.Clients;
using Banking.Model.Entities;

namespace Banking.Api
{
    public class AutoMapperConfiguration : Profile
    {
        public override string ProfileName => GetType().Name;

        public AutoMapperConfiguration()
        {
            CreateMap<Banking.Contract.Bank, Bank>().ReverseMap();
            CreateMap<Banking.Contract.User, User>().ReverseMap();
            CreateMap<Banking.Contract.Account, Account>().ReverseMap();
            CreateMap<BizfiBankTransaction, Banking.Contract.Transaction>()
                .ForMember(x => x.Amount, expression => expression.MapFrom(y => Math.Abs(y.amount)))
                .ForMember(x => x.TypeDescription, expression => expression.MapFrom(y => y.amount < 0 ? Banking.Contract.TransactionType.Debit.ToString() : Banking.Contract.TransactionType.Credit.ToString()))
                .ForMember(x => x.Type, expression => expression.MapFrom(y => y.amount < 0 ? Banking.Contract.TransactionType.Debit : Banking.Contract.TransactionType.Credit))
                .ForMember(x => x.Date, expression => expression.MapFrom(y => y.cleared_date));
            CreateMap<FairWayBankTransaction, Banking.Contract.Transaction>()
                .ForMember(x => x.Amount, expression => expression.MapFrom(y => y.amount))
                .ForMember(x => x.TypeDescription, expression => expression.MapFrom(y => Enum.Parse(typeof(Banking.Contract.TransactionType), y.type).ToString()))
                .ForMember(x => x.Type, expression => expression.MapFrom(y => Enum.Parse(typeof(Banking.Contract.TransactionType), y.type)))
                .ForMember(x => x.Date, expression => expression.MapFrom(y => y.bookedDate));
        }
    }
}