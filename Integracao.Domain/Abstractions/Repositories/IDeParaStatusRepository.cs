

using Integracao.Domain.Entities;

namespace Integracao.Domain.Abstractions.Repositories;

public interface IDeParaStatusRepository
{
    ValueTask<TDeParaStatus> GetDeParaAzureAsync(long acodStatusAzure);
    ValueTask<TDeParaStatus> GetDeParaGlpiAsync(long acodStatusGlpi);
}
