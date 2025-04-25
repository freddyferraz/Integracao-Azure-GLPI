using Integracao.Application.UseCases.Azures;
using Integracao.Domain.Shared;
using Integracao.Domain.ValueObjects.Azure;

namespace Integracao.Application.Abstractions;
public interface IAzureServices
{
    ValueTask<Result<CardAzureResponse>> RegistraCardAzure(CardAzureRequest request, string operacao, long acodAzure);
}
