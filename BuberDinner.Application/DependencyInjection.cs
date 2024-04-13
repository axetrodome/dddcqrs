using Microsoft.Extensions.DependencyInjection;
using MediatR;
namespace BuberDinner.Application;


public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //commented out, because this one has been replaced by MediatR
        // services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
        // services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}