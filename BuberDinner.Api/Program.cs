using BuberDinner.Api;
using BuberDinner.Api.Filters;
using BuberDinner.Api.Middleware;
using BuberDinner.Application;
using BuberDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    // builder.Services.AddControllers(
    //     options => options.Filters.Add<ErrorHandlingFilterAttribute>());'

    //to add custom propery on problem details factory
}

var app = builder.Build();
{
    //commented the code out since we're using Filter
    // app.UseMiddleware<ErrorHandlingMiddleware>();

    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}


