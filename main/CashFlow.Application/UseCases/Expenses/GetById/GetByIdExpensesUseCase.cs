using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

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

            if (result is null)
            {
                throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
            }

            return _mapper.Map<ResponseLongExpenseJson>(result);
            
        }

    }
}
