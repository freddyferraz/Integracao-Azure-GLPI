using Microsoft.Extensions.DependencyInjection;
using Integracao.Application.Abstractions;


namespace Integracao.Application;

public static class LoadDependencies
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //services.AddScoped<ICommandHandler<IncluirUsuarioCommand, IncluirUsuarioCommandResponse>, IncluirUsuarioCommandHandler>();


        return services;
    }
}
