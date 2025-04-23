using Integracao.Application.Abstractions;

namespace Integracao.Infra.Data;

public sealed class UnityOfWork(SqlServerDbContext sqlServerDbContext) : IUnityOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await sqlServerDbContext.SaveChangesAsync(cancellationToken);
    }
}
