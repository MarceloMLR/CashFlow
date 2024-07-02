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

        [Fact]
        public async Task Error_Email_Already_Exist()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase(request.Email);

            var act = async () => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
        }

        private RegisterUserUseCase CreateUseCase(string? email = null)
        {
            var mapper = MapperBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var userWriteOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
            var tokenGenerator = AccessTokenGeneratorBuilder.Build();
            var passwordEncripter = new PasswordEncripterBuilder().Build();
            var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder();

            if (string.IsNullOrWhiteSpace(email) == false) 
            {
                userReadOnlyRepository.ExistActiveUserWithEmail(email);
            }

            return new RegisterUserUseCase(mapper,passwordEncripter, userReadOnlyRepository.Build(), tokenGenerator, userWriteOnlyRepository, unitOfWork);
        }
    }
}
