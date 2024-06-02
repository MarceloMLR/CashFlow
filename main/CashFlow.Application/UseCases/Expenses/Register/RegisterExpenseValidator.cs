using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register;


public class RegisterExpenseValidator : AbstractValidator<RequestExpenseJson>
{
    public RegisterExpenseValidator()
    {
        RuleFor(expense => expense.Title).NotEmpty().WithMessage("Title is not defined");
        RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage("The Amount must be greater than zero");
        RuleFor(expense => expense.Date).LessThan(DateTime.UtcNow).WithMessage("Expenses cannot be the future");
        RuleFor(expense => expense.Type).IsInEnum().WithMessage("Payment Type is not valid.");
    }
}
