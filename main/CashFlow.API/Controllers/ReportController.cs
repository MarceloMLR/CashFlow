using CashFlow.Application.UseCases.Reports.Excel;
using CashFlow.Application.UseCases.Reports.Pdf;
using CashFlow.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.Api.Controllers
{
    [Authorize(Roles = Roles.ADMIN)]
    public class ReportController : CashFlowBaseController
    {
        [HttpGet("excel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        
        public async Task<IActionResult> GetExcel(
            [FromServices] IGenerateExpensesReportExcelUseCase useCase,
            [FromHeader] DateOnly month)
        {

            byte[] file = await useCase.Execute(month);

            if(file.Length > 0)
            {
                return File(file, MediaTypeNames.Application.Octet, "Report.xlsx");
            }

            return NoContent();
            
        }

        [HttpGet("pdf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPdf(
           [FromServices] IGenerateExpensesReportPdfUseCase useCase,
           [FromHeader] DateOnly month)
        {
            byte[] file = await useCase.Execute(month);

            if (file.Length > 0)
            {
                return File(file, MediaTypeNames.Application.Pdf, "Report.pdf");
            }

            return NoContent();

        }
    }
}
