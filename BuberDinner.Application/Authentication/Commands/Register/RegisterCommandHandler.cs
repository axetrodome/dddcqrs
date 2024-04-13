namespace BuberDinner.Application.Authentication.Commands.Register;

using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;
using MediatR;



public class RegisterCommandHandler : 
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        //just to get rid of the annoying warning since we're not doing anything async here
        await Task.CompletedTask;

        if (_userRepository.GetUserByEmail(command.Email) != null) {
            return Errors.User.DuplicateEmail; 
            // throw new DuplicateEmailException();
        }

        var user = new User{
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
        };

        _userRepository.AddUser(user);

        var Token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(user, Token);
    }
}