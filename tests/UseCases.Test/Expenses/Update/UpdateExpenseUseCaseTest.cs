using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CommonTestsUtilities.cs.AutoMapper;
using CommonTestsUtilities.cs.Entities;
using CommonTestsUtilities.cs.LoggedUser;
using CommonTestsUtilities.cs.Repositories;
using CommonTestsUtilities.cs.Requests;
using FluentAssertions;
using Xunit;

namespace UseCases.Tests.Expenses.Update
{
    public class UpdateExpenseUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();
            var request = RequestExpenseJsonBuilder.Build();
            var expense = ExpenseBuilder.Build(loggedUser);

            var useCase = CreateUseCase(loggedUser, expense);

            var act = async () => await useCase.Execute(expense.Id, request);

            await act.Should().NotThrowAsync();

            expense.Title.Should().Be(expense.Title);
            expense.Description.Should().Be(expense.Description);   
            expense.Date.Should().Be(expense.Date);
            expense.Amount.Should().Be(expense.Amount);
            expense.PaymentType.Should().Be((CashFlow.Domain.Enums.PaymentType) expense.PaymentType);

        }

        [Fact]
        public async Task Error_Title_Not_Found()
        {
            var loggedUser = UserBuilder.Build();
            var expense = ExpenseBuilder.Build(loggedUser);

            var request = RequestExpenseJsonBuilder.Build();

            request.Title = string.Empty;

            var useCase = CreateUseCase(loggedUser, expense);

            var act = async () => await useCase.Execute(expense.Id, request);

            var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.TITLE_REQUIRED));
        }

        [Fact] 
        public async Task Error_Expense_Not_Found()
        {
            var loggedUser = UserBuilder.Build();

            var request = RequestExpenseJsonBuilder.Build();

            var useCase = CreateUseCase(loggedUser);

            var act = async () => await useCase.Execute(id: 1000, request);

            var result = await act.Should().ThrowAsync<NotFoundException>();

            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EXPENSE_NOT_FOUND));
        }

        private UpdateExpenseUseCase CreateUseCase(User user, Expense? expense = null)
        {
            var repository = new ExpensesUpdateOnlyRepositoryBuilder().GetById(user, expense).Build();
            var mapper = MapperBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            return new UpdateExpenseUseCase(unitOfWork, mapper, repository, loggedUser);
        }
    };


 
}
