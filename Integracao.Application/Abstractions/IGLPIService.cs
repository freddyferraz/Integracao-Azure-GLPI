using Integracao.Domain.Shared;
using Integracao.Domain.ValueObjects.GLPI;

namespace Integracao.Application.Abstractions;
public interface IGLPIService
{
    ValueTask<Result<string>> IniciarSessaoGLPI(string authToken);
    ValueTask<bool> FinalizarSessaoGLPI(string authToken, string sessionToken);
    ValueTask<Result<bool>> AtualizaStatusGLPI(long acodGlpi, int status, string sessionToken, string authToken);
    ValueTask<Result<RetornoTicketGlpiResponse>> AtualizaTicketGLPI(long acodGlpi, string comentario, string sessionToken, string authToken);
}
