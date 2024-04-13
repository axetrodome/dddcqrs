using System.Net;

namespace BuberDinner.Application.Common.Errors;

public interface IError
{
    public HttpStatusCode httpStatusCode { get; }
    public string ErrorMessage { get; }    
}