
using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CashFlow.Api.Controllers;


[Authorize]
public class ExpensesController : CashFlowBaseController
{

    [HttpPost]
    [SwaggerOperation(Summary = "Registra uma nova despesa")]
    [ProducesResponseType(typeof(ResponseRegisteredExpenseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    
    public async Task<IActionResult> Register(
        [FromServices] IRegisterExpenseUseCase useCase,
        RequestExpenseJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);

    }


    [HttpGet]
    [SwaggerOperation(Summary = "Retorna todas as despesas")]
    [ProducesResponseType(typeof(ResponseExpensesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllExpenses([FromServices] IGetAllExpensesUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response.ExpensesList.Count > 0)
        {
            return Ok(response);
        }

        return NoContent();
    }

    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Retorna uma despesa pelo id")]
    [ProducesResponseType(typeof(ResponseLongExpenseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetExpenseById([FromServices] IGetByIdExpensesUseCase useCase, [FromRoute] long id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }


    [HttpDelete]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Deleta uma despesa")]
    [ProducesResponseType(typeof(ResponseLongExpenseJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete([FromServices] IDeleteExpenseUseCase useCase, [FromRoute] long id)
    {
        await useCase.Execute(id);

        return NoContent();
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Atualiza uma despesa existente")]
    [ProducesResponseType(typeof(ResponseLongExpenseJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [Route("{id}")]
    public async Task<IActionResult> Update(
       [FromServices] IUpdateExpenseUseCase useCase,
       [FromRoute] long id,
       [FromBody] RequestExpenseJson request) 
        {
        await useCase.Execute(id, request);
        return NoContent();
        }
}
