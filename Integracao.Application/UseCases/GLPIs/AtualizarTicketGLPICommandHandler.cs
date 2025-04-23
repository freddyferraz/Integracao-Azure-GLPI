using Integracao.Application.Abstractions;
using Integracao.Domain.Abstractions.Repositories;
using Integracao.Domain.Errors;
using Integracao.Domain.Shared;

namespace Integracao.Application.UseCases.GLPIs;
internal class AtualizarTicketGLPICommandHandler(ITicketsRepository ticketsRepository, IDeParaStatusRepository deParaStatusRepository, IUsuarioRepository usuarioRepository, 
                                                 IUnityOfWork unityOfWork, IGLPIService gLPIService) 
                                                : ICommandHandler<AtualizarTicketGLPICommand, AtualizarTicketGLPIResponse>
{
    public async ValueTask<Result<AtualizarTicketGLPIResponse>>HandleAsync(AtualizarTicketGLPICommand command , CancellationToken cancellationToken)
    {
        #region Validações Registros Existentes
        var usuario = await usuarioRepository.GetUsuarioByEmail(command.email);
        if(usuario is null)
        {
            return Result.Failure<AtualizarTicketGLPIResponse>(UsuarioErrors.EmailUsuarioNaoEncontrado);
        }

        var deParaStatus = await deParaStatusRepository.GetDeParaAzureAsync(command.acodTicketAzure);
        if(deParaStatus is null)
        {
            return Result.Failure<AtualizarTicketGLPIResponse>(DeParaStatusErrors.StatusAzureNaoEncontrado);
        }

        var ticket = await ticketsRepository.GetTicketByIdAzureAsync(command.acodTicketAzure, cancellationToken);
        if(ticket is null)
        {
            return Result.Failure<AtualizarTicketGLPIResponse>(TicketErrors.IdAzureNaoEncontrado);
        }
        #endregion Validações Registros Existentes

        var sessao = await gLPIService.IniciarSessaoGLPI(command.token);

        #region Verificação Alteração Status
        if(ticket.AcodStatus != command.status)
        {
            var updateStatus = await gLPIService.AtualizaStatusGLPI(ticket.AcodTicketsglpi, deParaStatus.AcodStatusGlpi, sessao.Value, command.token);
        }
        #endregion Verificação Alteração Status

        var updateConteudo = await gLPIService.AtualizaTicketGLPI(ticket.AcodTicketsglpi, command.content, sessao.Value, command.token);

        var result = new AtualizarTicketGLPIResponse(true, updateConteudo.Value.message);

        return Result.Success(result);
    }
}
