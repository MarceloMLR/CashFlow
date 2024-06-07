using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses
{
    public interface IExpensesWriteOnlyRepository
    {
        Task Add(Expense expense);
        /// <summary>
        /// THis Function returns TRUE if the deletion was successful otherwise return FALSE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool>  Delete(long id);
    }
}
