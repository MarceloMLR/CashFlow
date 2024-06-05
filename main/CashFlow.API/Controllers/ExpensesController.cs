using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

public class ExpensesController : CashFlowBaseController
{
    [HttpPost]

    public async Task<IActionResult> Register(
        [FromServices] IRegisterExpenseUseCase useCase,
        RequestExpenseJson request)
    {

        var response = await useCase.Execute(request);

        return Created(string.Empty, response);

    }
}
