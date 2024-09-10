using FluentAssertions;
using System.Net;
using System.Text.Json;
using WebApi.Test;
using Xunit;

namespace WebApi.Tests.Expenses.GetAll;


public class GetAllExpensesTest : CashFlowClassFixture
{
    private const string METHOD = "cashflow/expenses";

    private readonly string _token;

    public GetAllExpensesTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoGet(requestUri: METHOD, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("expensesList").EnumerateArray().Should().NotBeNullOrEmpty();
    }
}
