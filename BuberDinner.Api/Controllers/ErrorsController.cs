namespace BuberDinner.Api.Controllers;

using BuberDinner.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public class ErrorsController : ApiController

{

    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        //using c# switch expression
        var (statusCode, message) = exception switch 
        {
            IServiceException serviceException => ((int)serviceException.httpStatusCode, serviceException.ErrorMessage),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occured.")
        };

        return Problem(statusCode: statusCode, title: message); 
    }
}