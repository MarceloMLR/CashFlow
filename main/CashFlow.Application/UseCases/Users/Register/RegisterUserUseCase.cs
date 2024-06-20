using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Users.Register;


public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    public RegisterUserUseCase(IMapper mapper, IPasswordEncripter encripter, IUserReadOnlyRepository userReadOnlyRepository)
    {
        _mapper = mapper;
        _passwordEncripter = encripter;
        _userReadOnlyRepository = userReadOnlyRepository;
    }
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user =  _mapper.Map<Domain.Entities.User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password);

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);

        var emailExists = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExists)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
        }

        if (result.IsValid == false) 
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
