namespace BuberDinner.Application.Services.Authentication.Commands;

using ErrorOr;

public interface IAuthenticationCommandService
{
    ErrorOr<AuthenticationResult> Register(string FirstName, string LastName, string Email, string Password);
}


