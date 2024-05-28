using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

public class ExpensesController : CashFlowBaseController
{
    [HttpPost]

    public IActionResult Register(RequestExpenseJson request)
    {
        return Ok();
    }
}
