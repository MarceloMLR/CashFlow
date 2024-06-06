using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetById
{

    public class GetByIdExpensesUseCase : IGetByIdExpensesUseCase
    {
        private readonly IExpensesRepository _expensesRepository;
        private readonly IMapper _mapper;
        public GetByIdExpensesUseCase(IExpensesRepository repository, IMapper mapper)
        {
            _expensesRepository = repository;
            _mapper = mapper;
        }

      

        public async Task<ResponseLongExpenseJson> Execute(long id)
        {
            var result = await _expensesRepository.GetById(id);

            return _mapper.Map<ResponseLongExpenseJson>(result);
            
        }

    }
}
