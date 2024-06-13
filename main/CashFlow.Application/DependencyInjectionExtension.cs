﻿using CashFlow.Application.AutoMapper;
using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Application.UseCases.Reports.Excel;
using CashFlow.Application.UseCases.Reports.Pdf;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddAplication(this IServiceCollection services)
        {
            AddUseCase(services);
            AddAutoMapper(services);
        }

        private static void AddUseCase(IServiceCollection services)
        {
            services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
            services.AddScoped<IGetAllExpensesUseCase, GetAllExpensesUseCase>();
            services.AddScoped<IGetByIdExpensesUseCase, GetByIdExpensesUseCase>();
            services.AddScoped<IDeleteExpenseUseCase, DeleteExpenseUseCase>();
            services.AddScoped<IUpdateExpenseUseCase, UpdateExpenseUseCase>();
            services.AddScoped<IGenerateExpensesReportExcelUseCase, GenerateExpensesReportExcelUseCase>();
            services.AddScoped<IGenerateExpensesReportPdfUseCase, GenerateExpensesReportPdfUseCase>();
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapping));
        }
    }
}
