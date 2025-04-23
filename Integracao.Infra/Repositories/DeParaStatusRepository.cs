using Integracao.Domain.Abstractions.Repositories;
using Integracao.Domain.Entities;
using Integracao.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Integracao.Infra.Repositories;

public sealed class DeParaStatusRepository(SqlServerDbContext sqlServerDbContext) : IDeParaStatusRepository
{

    public async ValueTask<TDeParaStatus> GetDeParaAzureAsync(long acodStatusAzure)
    {
        var data = await sqlServerDbContext.DeParaStatus.Where(t => t.AcodStatusDevops == acodStatusAzure).FirstOrDefaultAsync();

        return data!;

    }
    public async ValueTask<TDeParaStatus> GetDeParaGlpiAsync(long acodStatusAzure)
    {
        var data = await sqlServerDbContext.DeParaStatus.Where(t => t.AcodStatusGlpi == acodStatusAzure).FirstOrDefaultAsync();

        return data!;

    }

}