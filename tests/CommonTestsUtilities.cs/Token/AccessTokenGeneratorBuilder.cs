using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Tokens;
using Moq;

namespace CommonTestsUtilities.cs.Token
{
    public class AccessTokenGeneratorBuilder
    {
        public static IAccessTokenGenerator Build()
        {
            var mock = new Mock<IAccessTokenGenerator>();

            mock.Setup(config => config.Generate(It.IsAny<User>())).Returns("token");

            return mock.Object;
        }
    }
}
