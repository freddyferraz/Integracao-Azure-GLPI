using Microsoft.Extensions.DependencyInjection;
using Integracao.Application.Abstractions;
using Integracao.Application.UseCases.GLPIs;
using Integracao.Application.Services.GLPI;
using Integracao.Application.Services.Azure;
using Integracao.Application.UseCases.Azures;


namespace Integracao.Application;

public static class LoadDependencies
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IGLPIService, GLPIService>();
        services.AddScoped<IAzureServices, AzureServices>();

        //Uses Cases
        services.AddScoped<ICommandHandler<AtualizarTicketGLPICommand, AtualizarTicketGLPIResponse>, AtualizarTicketGLPICommandHandler>();
        services.AddScoped<ICommandHandler<AtualizarCardAzureCommand, AtualizarCardAzureResponse>,  AtualizarCardAzureCommandHandler>();
        

        return services;
    }
}
