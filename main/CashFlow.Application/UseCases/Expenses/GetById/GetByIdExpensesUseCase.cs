using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.GetById
{

    public class GetByIdExpensesUseCase : IGetByIdExpensesUseCase
    {
        private readonly IExpensesReadOnlyRepository _expensesRepository;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        public GetByIdExpensesUseCase(
            IExpensesReadOnlyRepository repository, 
            IMapper mapper,
            ILoggedUser loggedUser
            )
        {
            _expensesRepository = repository;
            _mapper = mapper;
            _loggedUser = loggedUser;
        }

      

        public async Task<ResponseLongExpenseJson> Execute(long id)
        {
            var loggedUser = await _loggedUser.Get();

            var result = await _expensesRepository.GetById(loggedUser, id);

            if (result is null)
            {
                throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
            }

            return _mapper.Map<ResponseLongExpenseJson>(result);
            
        }

    }
}
