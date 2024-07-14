﻿using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Delete
{
    public class DeleteExpenseUseCase : IDeleteExpenseUseCase
    {
        private readonly IExpensesWriteOnlyRepository _repository;
        private readonly IExpensesReadOnlyRepository _repositoryReadOnly;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggedUser _loggedUser;

        public DeleteExpenseUseCase(
            IExpensesWriteOnlyRepository repository,
            IExpensesReadOnlyRepository repositoryReadOnly,
            IUnitOfWork unitOfWork,
            ILoggedUser loggedUser
            )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _loggedUser = loggedUser;
            _repositoryReadOnly = repositoryReadOnly;
        }
        public async Task  Execute(long id)
        {
            var loggedUser = await _loggedUser.Get();

            var expense = await _repositoryReadOnly.GetById(loggedUser, id);

            if (expense is null)
            {
                throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
            }

            await _repository.Delete(id);

            

            await _unitOfWork.Commit();
        }
    }
}
