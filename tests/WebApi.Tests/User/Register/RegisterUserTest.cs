using CommonTestsUtilities.cs.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace WebApi.Tests.User.Register
{
    public class RegisterUserTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private const string METHOD = "cashflow/user";
        public RegisterUserTest(CustomWebApplicationFactory webApplicationFactory)
        {
            _httpClient = webApplicationFactory.CreateClient();
           
        }

        [Fact]
        public async Task Success()
        {
           var request = RequestRegisterUserJsonBuilder.Build();

           var result = await _httpClient.PostAsJsonAsync(METHOD, request);

            result.StatusCode.Should().Be(HttpStatusCode.Created);

        }
    }
}
