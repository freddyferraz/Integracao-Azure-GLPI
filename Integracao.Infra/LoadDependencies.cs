using Integracao.Application.Abstractions;
using Integracao.Domain.Abstractions.Repositories;
using Integracao.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Integracao.Infra.Data;

public static class LoadDependencies
{
    public static IServiceCollection AddInfraestructureData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SqlServerDbContext>(options =>
        {
            options.UseSqlServer(configuration["SQL_SERVER_CONNECTION_STRING"]);
        });

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IDeParaStatusRepository, DeParaStatusRepository>();
        services.AddScoped<ITicketsRepository, TicketsRepository>();
        

        services.AddScoped<IUnityOfWork, UnityOfWork>();

        return services;
    }
}