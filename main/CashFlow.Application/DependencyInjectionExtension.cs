using CashFlow.Application.AutoMapper;
using CashFlow.Application.UseCases.Expenses.Register;
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
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapping));
        }
    }
}
