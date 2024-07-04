using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using CashFlow.Infrastructure.Security.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddRepositories(services);
            AddToken(services, configuration);

            services.AddScoped<IPasswordEncripter, Security.BCrypt>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IExpensesReadOnlyRepository, ExpensesRepository>();
            services.AddScoped<IExpensesWriteOnlyRepository, ExpensesRepository>();
            services.AddScoped<IExpensesUpdateOnlyRepository, ExpensesRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void AddToken(IServiceCollection services, IConfiguration configuration)
        {
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:jwt:ExpiresMinutes");
            var signingKey = configuration.GetValue<string>("Settings:jwt:SigningKey");

            services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            

            var connectionString = configuration.GetConnectionString("Connection");
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            services.AddDbContext<CashFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));


        }
    }
}
