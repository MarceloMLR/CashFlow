﻿using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;


public interface IExpensesUpdateOnlyRepository
{
    public Task<Expense?> GetById(Entities.User user, long id);
    void Update(Expense expense) { }
}