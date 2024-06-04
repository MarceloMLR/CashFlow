using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

public class ExpensesController : CashFlowBaseController
{
    [HttpPost]

    public IActionResult Register(
        [FromServices] IRegisterExpenseUseCase useCase,
        RequestExpenseJson request)
    {

        var response = useCase.Execute(request);

        return Created(string.Empty, response);

    }
}
