﻿namespace CashFlow.Application.UseCases.Reports.Excel
{
    public interface IGenerateExpensesReportExcelUseCase
    {
        public Task<byte[]> Execute(DateOnly month);
       
    }
}
