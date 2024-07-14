﻿using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UseCases.Expenses.GetAll
{
    public class GetAllExpensesUseCase : IGetAllExpensesUseCase
    {
        private readonly IExpensesReadOnlyRepository _expensesRepository;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        public GetAllExpensesUseCase(
            IExpensesReadOnlyRepository repository, 
            IMapper mapper,
            ILoggedUser loggedUser
            )
        {
            _expensesRepository = repository;
            _mapper = mapper;   
            _loggedUser = loggedUser;
        }
        public async Task<ResponseExpensesJson> Execute()
        {
           var loggedUser = await _loggedUser.Get();

           var result = await _expensesRepository.GetAll(loggedUser);

            return new ResponseExpensesJson
            {
                ExpensesList = _mapper.Map<List<ResponseShortExpenseJson>>(result)
            };
        }

        
    }
}
