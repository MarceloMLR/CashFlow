using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Users.Register;


public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;
    public RegisterUserUseCase(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        Validate(request);

        var user =  _mapper.Map<Domain.Entities.User>(request);

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
        };
    }

    private void Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);

        if (result is null) 
        {
            var errorMessages = result.Errors.Select(e => e.Message).ToList();
            
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
