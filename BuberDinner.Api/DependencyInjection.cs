namespace BuberDinner.Api;

using BuberDinner.Api.Common.Mapping;
using BuberDinner.Api.Common.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMappings();
        services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();
        services.AddControllers();

        return services;
    }
}