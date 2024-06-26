﻿using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAll
{
    public class GetAllExpensesUseCase : IGetAllExpensesUseCase
    {
        private readonly IExpensesReadOnlyRepository _expensesRepository;
        private readonly IMapper _mapper;
        public GetAllExpensesUseCase(IExpensesReadOnlyRepository repository, IMapper mapper)
        {
            _expensesRepository = repository;
            _mapper = mapper;   
        }
        public async Task<ResponseExpensesJson> Execute()
        {
           var result = await _expensesRepository.GetAll();

            return new ResponseExpensesJson
            {
                ExpensesList = _mapper.Map<List<ResponseShortExpenseJson>>(result)
            };
        }

        
    }
}
