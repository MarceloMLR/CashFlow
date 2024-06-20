using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToEntity();
            EntityToResponse();
        }

        private void RequestToEntity()
        {
            CreateMap<RequestExpenseJson, Expense>()
                .ForMember(dest => dest.PaymentType, source => source.MapFrom(src => src.Type));

            CreateMap<RequestRegisterUserJson, User>();
        }

        private void EntityToResponse()
        {
            CreateMap<Expense, ResponseRegisteredExpenseJson>();
            CreateMap<Expense, ResponseShortExpenseJson>();
            CreateMap<Expense, ResponseLongExpenseJson>()
                .ForMember(dest => dest.Type, source => source.MapFrom(src => src.PaymentType));
               
        }

     
    }
}
