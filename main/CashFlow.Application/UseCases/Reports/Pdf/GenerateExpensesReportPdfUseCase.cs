using CashFlow.Application.UseCases.Reports.Pdf.Fonts;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;
using Document = MigraDoc.DocumentObjectModel.Document;
using Font = MigraDoc.DocumentObjectModel.Font;

namespace CashFlow.Application.UseCases.Reports.Pdf;


public class GenerateExpensesReportPdfUseCase : IGenerateExpensesReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private readonly IExpensesReadOnlyRepository _repository;

    public GenerateExpensesReportPdfUseCase(IExpensesReadOnlyRepository repository)
    {
        _repository = repository;

        GlobalFontSettings.FontResolver = new ExpensesReportFontResolver();
    }
    public async Task<byte[]> Execute(DateTime month)
    {
        var expenses = await _repository.FilterByMonth(month);

        if (expenses.Count == 0)
        {
            return [];
        }

        var totalExpenses = expenses.Sum(expense => expense.Amount);

        var document = CreateDocument(month);
        var page = CreateSection(document);

        CreateHeaderWithLogoAndName(page);
        CreateTotalSpentSection(page, month, totalExpenses);




        return RenderDocument(document);
    }

    private Document CreateDocument(DateTime month)
    {
        var document = new Document();

        document.Info.Title = $"{ResourceReportGenerationMessages.EXPENSES_FOR} {month.ToString("Y")}";
        document.Info.Author = "Marcelo Lima";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;



        return document;
    }

    private Section CreateSection(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.BottomMargin = 80;

        return section;
    }

    private void CreateHeaderWithLogoAndName(Section page)
    {
        var table = page.AddTable();
        table.AddColumn();
        table.AddColumn(300);

        var row = table.AddRow();

        var assembly = Assembly.GetExecutingAssembly();
        var pathDirectory = Path.GetDirectoryName(assembly.Location);
        var pathFile = Path.Combine(pathDirectory!, "Logo", "user.png");


        row.Cells[0].AddImage(pathFile);

        row.Cells[1].AddParagraph("Olá, Marcelo Lima");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };
        row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
    }

    private void CreateTotalSpentSection(Section page, DateTime month, decimal totalExpenses)
    {
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "40";

        var title = string.Format(ResourceReportGenerationMessages.TOTAL_SPENT_IN, month.ToString("Y"));

        paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });
        paragraph.AddLineBreak();

       

        paragraph.AddFormattedText($"{totalExpenses} {CURRENCY_SYMBOL}", new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });
    }

    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document,
        };

        renderer.RenderDocument();

        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);

        return file.ToArray();
    }
}
