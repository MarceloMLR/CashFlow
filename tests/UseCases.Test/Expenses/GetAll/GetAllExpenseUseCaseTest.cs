using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CommonTestsUtilities.cs.AutoMapper;
using CommonTestsUtilities.cs.Entities;
using CommonTestsUtilities.cs.LoggedUser;
using CommonTestsUtilities.cs.Repositories;
using FluentAssertions;
using Xunit;

namespace UseCases.Tests.Expenses.GetAll;


public class GetAllExpenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var expenses = ExpenseBuilder.Collection(loggedUser);

        var useCase = CreateUseCase(loggedUser, expenses);

        var result = await useCase.Execute();

        result.ExpensesList.Should().NotBeNullOrEmpty().And.AllSatisfy(expense =>
            {
                expense.Id.Should().BeGreaterThan(0);
                expense.Title.Should().NotBeNullOrEmpty();
                expense.Amount.Should().BeGreaterThan(0);
            }
         );
    }

    private GetAllExpensesUseCase CreateUseCase(User user, List<Expense> expenses)
    {
        var repository = new ExpensesReadOnlyRepositoryBuilder().GetAll(user, expenses).Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GetAllExpensesUseCase(repository, mapper, loggedUser);  
    } 
}
