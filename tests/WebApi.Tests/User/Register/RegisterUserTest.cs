using CommonTestsUtilities.cs.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace WebApi.Tests.User.Register
{
    public class RegisterUserTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        public RegisterUserTest(WebApplicationFactory<Program> webApplicationFactory)
        {
            _httpClient = webApplicationFactory.CreateClient();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();


        }
    }
}
