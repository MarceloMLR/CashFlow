using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Register;


public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpensesRepository _repository;
    public RegisterExpenseUseCase(IExpensesRepository repository)
    {
        _repository = repository;
    }
    public ResponseRegisteredExpenseJson Execute(RequestExpenseJson request)
    {
        Validate(request);

        var entity = new Expense
        {
            Title = request.Title,
            Description = request.Description,
            Date = request.Date,
            Amount = request.Amount,
            PaymentType = (Domain.Enums.PaymentType)request.Type,

        };

        _repository.Add(entity);
        
        return new ResponseRegisteredExpenseJson();
    }

    private void Validate(RequestExpenseJson request)
    {
        var validator = new RegisterExpenseValidator();

        var result = validator.Validate(request);

        

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
