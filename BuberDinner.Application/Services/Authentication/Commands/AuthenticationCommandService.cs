namespace BuberDinner.Application.Services.Authentication.Commands;

using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;


public class AuthenticationCommandService : IAuthenticationCommandService
{

    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    private readonly IUserRepository _userRepository;

    public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public ErrorOr<AuthenticationResult> Register(string FirstName, string LastName, string Email, string Password)
    {
        if (_userRepository.GetUserByEmail(Email) != null) {
            return Errors.User.DuplicateEmail; 
            // throw new DuplicateEmailException();
        }

        var user = new User{
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Password = Password
        };

        _userRepository.AddUser(user);

        var Token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(user, Token);

    }
}


