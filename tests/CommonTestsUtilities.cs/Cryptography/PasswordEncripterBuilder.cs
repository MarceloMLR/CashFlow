using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestsUtilities.cs.Cryptography
{
    public class PasswordEncripterBuilder
    {
        public static IPasswordEncripter Build()
        {
            var mock = new Mock<IPasswordEncripter>();

            mock.Setup(config => config.Encrypt(It.IsAny<string>())).Returns("!@#2%$$1351353");

            return mock.Object;
        }
    }
}
