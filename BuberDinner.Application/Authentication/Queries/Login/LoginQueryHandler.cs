namespace BuberDinner.Application.Authentication.Commands.Login;

using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Query.Login;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;
using MediatR;



public class LoginQueryHandler : 
    IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        //just to get rid of the annoying warning since we're not doing anything async here
        await Task.CompletedTask;

        if (_userRepository.GetUserByEmail(query.Email) is not User user) {
            return Errors.Authentication.InvalidCredentials;
        }

        //improve the password checker and also on how we're storing the password
        if (user.Password != query.Password) {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);

    }
}