using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.Api.Controllers
{

    public class ReportController : CashFlowBaseController
    {
        [HttpGet("excel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetExcel()
        {
            byte[] file = new byte[1];

            if(file.Length > 0)
            {
                return File(file, MediaTypeNames.Application.Octet, "Report.xlsx");
            }

            return NoContent();
            
        }
    }
}
