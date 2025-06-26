using CashFlow.Application.UseCases.Reports.Excel;
using CashFlow.Domain.Entities;
using CommonTestsUtilities.cs.Entities;
using CommonTestsUtilities.cs.LoggedUser;
using CommonTestsUtilities.cs.Repositories;
using FluentAssertions;
using Xunit;

namespace UseCases.Tests.Expenses.Reports.Excel;


public class GenerateExpensesReportExcelUseCaseTest
{

    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var expenses = ExpenseBuilder.Collection(loggedUser);

        var useCase = CreateUseCase(loggedUser, expenses);

        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

        result.Should().NotBeNullOrEmpty();


    }

    [Fact]
    public async Task Success_Empty()
    {
        var loggedUser = UserBuilder.Build();

        var useCase = CreateUseCase(loggedUser, new List<Expense>());

        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

        result.Should().BeEmpty();
    }

    private GenerateExpensesReportExcelUseCase CreateUseCase(User user, List<Expense> expenses)
    {
        var repository = new ExpensesReadOnlyRepositoryBuilder().FilterByMonth(user, expenses).Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GenerateExpensesReportExcelUseCase(repository, loggedUser);
    }
}
