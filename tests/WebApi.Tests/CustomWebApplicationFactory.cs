﻿using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Infrastructure.DataAccess;
using CommonTestsUtilities.cs.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Tests;


public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private CashFlow.Domain.Entities.User _user;
    private  string _password;
    private string _token;
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test").
            ConfigureServices(services =>
            {
                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<CashFlowDbContext>(config =>
                {
                    config.UseInMemoryDatabase("InMemoryDbForTesting");
                    config.UseInternalServiceProvider(provider);
                });

                var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<CashFlowDbContext>();
                var passwordEncripter = scope.ServiceProvider.GetRequiredService<IPasswordEncripter>();
                var tokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();

                StartDatabase(dbContext, passwordEncripter);

                _token = tokenGenerator.Generate(_user);
            });
    }

    public string GetEmail() => _user.Email;
    public string GetName() => _user.Name;
    public string GetPassword() => _password;
    public string GetToken() => _token;

    private void StartDatabase(CashFlowDbContext dbContext, IPasswordEncripter passwordEncripter)
    {
        _user = UserBuilder.Build();
        _password = _user.Password;

        _user.Password = passwordEncripter.Encrypt(_user.Password);

        dbContext.User.Add(_user);

        dbContext.SaveChanges();
    }
}
