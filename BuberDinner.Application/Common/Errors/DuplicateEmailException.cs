namespace BuberDinner.Application.Common.Errors;

using System.Net;

public class DuplicateEmailException : Exception, IServiceException
{
    public HttpStatusCode httpStatusCode => HttpStatusCode.Conflict;

    public string ErrorMessage => "User Already exists";
}