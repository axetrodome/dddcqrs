namespace BuberDinner.Api.Controllers;

using BuberDinner.Api.Filters;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Query.Login;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("auth")]

//commented this out since it's specified on Program.cs
// [ErrorHandlingFilter]
public class AuthenticationController : ApiController
{

    private readonly IMediator _mediator;

    private readonly IMapper _mapper;
    
    public AuthenticationController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [Route("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
                                    
        ErrorOr<AuthenticationResult> authenticationResult = await _mediator.Send(command);

        return authenticationResult.Match(
            authenticationResult => Ok(_mapper.Map<AuthenticationResult>(authenticationResult)),
            errors => Problem(errors)
        );
    }



    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {

        var query = _mapper.Map<LoginQuery>(request);

        var authenticationResult = await _mediator.Send(query);

        if (authenticationResult.IsError && authenticationResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authenticationResult.FirstError.Description);
        }

        return authenticationResult.Match(
            authenticationResult => Ok(_mapper.Map<AuthenticationResult>(authenticationResult)),
            errors => Problem(errors)
        );
    }
}
