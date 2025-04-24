using Microsoft.Extensions.DependencyInjection;
using Integracao.Application.Abstractions;
using Integracao.Application.UseCases.GLPIs;
using Integracao.Application.Services.GLPI;


namespace Integracao.Application;

public static class LoadDependencies
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IGLPIService, GLPIService>();
        services.AddScoped<ICommandHandler<AtualizarTicketGLPICommand, AtualizarTicketGLPIResponse>, AtualizarTicketGLPICommandHandler>();


        return services;
    }
}
