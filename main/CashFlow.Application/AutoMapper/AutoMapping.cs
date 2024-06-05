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
        }

        private void EntityToResponse()
        {
            CreateMap<Expense, ResponseRegisteredExpenseJson>();
            CreateMap<Expense, ResponseShortExpenseJson>();
               
        }

     
    }
}
