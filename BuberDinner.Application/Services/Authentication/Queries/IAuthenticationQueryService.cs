namespace BuberDinner.Application.Services.Authentication.Queries;

using ErrorOr;

public interface IAuthenticationQueryService
{
    ErrorOr<AuthenticationResult> Login(string Email, string Password);
}


