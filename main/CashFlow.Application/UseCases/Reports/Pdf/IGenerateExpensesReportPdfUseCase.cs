namespace CashFlow.Application.UseCases.Reports.Pdf;
public interface IGenerateExpensesReportPdfUseCase
{
    public Task<byte[]> Execute(DateTime month);
}
