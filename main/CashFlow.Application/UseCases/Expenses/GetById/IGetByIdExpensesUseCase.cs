using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.GetById
{
    public interface IGetByIdExpensesUseCase
    {
        public Task<ResponseLongExpenseJson> Execute(long id);
    }
}
