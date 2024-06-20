using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;
[Route("cashflow/[controller]")]
[ApiController]
public abstract class CashFlowBaseController : ControllerBase
{
}
