using System.Net;

namespace BuberDinner.Application.Common.Errors;

public record struct DuplicateEmailError() : IError
{
    public HttpStatusCode httpStatusCode => throw new NotImplementedException();

    public string ErrorMessage => throw new NotImplementedException();
}
