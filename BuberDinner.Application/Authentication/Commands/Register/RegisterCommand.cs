namespace BuberDinner.Application.Authentication.Commands.Register;

using BuberDinner.Application.Authentication.Common;
using ErrorOr;
using MediatR;


public record RegisterCommand(string FirstName,
                              string LastName,
                              string Email,
                              string Password) : IRequest<ErrorOr<AuthenticationResult>>;