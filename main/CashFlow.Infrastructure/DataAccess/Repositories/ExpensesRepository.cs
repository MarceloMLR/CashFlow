﻿using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories
{
    internal class ExpensesRepository : IExpensesReadOnlyRepository, IExpensesWriteOnlyRepository, IExpensesUpdateOnlyRepository
    {
        private readonly CashFlowDbContext _dbContext;
        public ExpensesRepository(CashFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(Expense expense)
        {
            await _dbContext.Expenses.AddAsync(expense);
        }

        public async Task<List<Expense>> GetAll(User user)
        {
            return await _dbContext.Expenses.AsNoTracking().Where(expense => expense.UserId == user.Id).ToListAsync();
        }


        async Task<Expense?> IExpensesReadOnlyRepository.GetById(User user, long id)
        {
            return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(expense => expense.Id == id && expense.UserId == user.Id);
        }

        public async Task Delete(long id)
        {
            var result = await _dbContext.Expenses.FindAsync(id);
  

            _dbContext.Expenses.Remove(result!);

        }

        public void Update(Expense expense)
        {
            _dbContext.Update(expense);
        }

        async Task<Expense?> IExpensesUpdateOnlyRepository.GetById(User user, long id)
        {
            return await _dbContext.Expenses.FirstOrDefaultAsync(expense => expense.Id == id && expense.UserId == user.Id);
        }

        public async Task<List<Expense>> FilterByMonth(DateTime date)
        {
            var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;

            var daysInMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);

            var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);

            return await _dbContext.Expenses.AsNoTracking().Where(expense => expense.Date >= startDate && expense.Date <= endDate)
                .OrderBy(expense => expense.Date)
                .ThenBy(expense => expense.Title)
                .ToListAsync();
        }
    }
}