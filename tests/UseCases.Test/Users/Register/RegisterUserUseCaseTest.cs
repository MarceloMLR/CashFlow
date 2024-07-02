using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CommonTestsUtilities.cs.AutoMapper;
using CommonTestsUtilities.cs.Cryptography;
using CommonTestsUtilities.cs.Repositories;
using CommonTestsUtilities.cs.Requests;
using CommonTestsUtilities.cs.Token;
using FluentAssertions;
using Xunit;

namespace UseCases.Test.Users.Register
{


    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Name.Should().Be(request.Name);
            result.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;

            var useCase = CreateUseCase();

            var act = async () => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.NAME_EMPTY));
        }

        private RegisterUserUseCase CreateUseCase()
        {
            var mapper = MapperBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var userWriteOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
            var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder().Build();
            var tokenGenerator = AccessTokenGeneratorBuilder.Build();
            var passwordEncripter = PasswordEncripterBuilder.Build();

            return new RegisterUserUseCase(mapper,passwordEncripter, userReadOnlyRepository, tokenGenerator, userWriteOnlyRepository, unitOfWork);
        }
    }
}
