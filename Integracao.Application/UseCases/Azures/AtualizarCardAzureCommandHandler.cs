using Integracao.Application.Abstractions;
using Integracao.Domain.Abstractions.Repositories;
using Integracao.Domain.Entities;
using Integracao.Domain.Errors;
using Integracao.Domain.Shared;
using Integracao.Domain.ValueObjects.Azure;

namespace Integracao.Application.UseCases.Azures;
internal class AtualizarCardAzureCommandHandler(ITicketsRepository ticketsRepository, IDeParaStatusRepository deParaStatusRepository, IUsuarioRepository usuarioRepository,
                                                 IUnityOfWork unityOfWork, IAzureServices azureServices)
                                                :ICommandHandler<AtualizarCardAzureCommand, AtualizarCardAzureResponse>
{
    public async ValueTask<Result<AtualizarCardAzureResponse>> HandleAsync(AtualizarCardAzureCommand command, CancellationToken cancellationToken)
    {
       //Inicialização de Variavel considerando que Card no Azure não existe.
        var operacao = "add";
        long ticketAzure = 0;
        var link = Environment.GetEnvironmentVariable("linkTicket") + $"{command.AcodTicketGLPI}";


        #region Validações Registros Existentes
        var usuario = await usuarioRepository.GetUsuarioById(command.Requerente);
        if (usuario is null)
        {
            return Result.Failure<AtualizarCardAzureResponse>(UsuarioErrors.EmailUsuarioNaoEncontrado);
        }

        var deParaStatus = await deParaStatusRepository.GetDeParaAzureAsync(command.Status);
        if (deParaStatus is null)
        {
            return Result.Failure<AtualizarCardAzureResponse>(DeParaStatusErrors.StatusAzureNaoEncontrado);
        }

        var ticket = await ticketsRepository.GetTicketByIdGlpiAsync(command.AcodTicketGLPI, cancellationToken);
        if(ticket is not null)
        {
            operacao = "replace";
            ticketAzure = ticket.AcodTicketsDevops;
        } 

        #endregion Validações Registros Existentes


        var request = new CardAzureRequest(operacao, command.Area, command.Title, command.Title, command.Description, usuario.AdesEmail, usuario.AdesEmail,
 usuario.AdesEmail, command.TipoChamado, command.CaterogiraChamado, command.Prioridade,
            command.Impacto, command.Urgencia, command.Localizacao, command.Data, link, deParaStatus.AdesStatusDevops);


        var updateConteudo = await azureServices.RegistraCardAzure(request, operacao, ticketAzure);


        #region Atualização/Criação do Ticket
        

        if (ticket is null)
        {
            var novoTicket = new Ticket
            {
                AcodTicketsglpi = command.AcodTicketGLPI,
                AdatAlteracao = DateTime.Now,
                AcodUsuario = usuario.AcodUsuario,
                AcodStatus = 1,
                AcodTicketsDevops = updateConteudo.Value.id

            };

            await ticketsRepository.InsertTicketAsync(novoTicket);
        }
        else
        {
           
            ticket.AcodStatus = deParaStatus.AcodStatus;

            await ticketsRepository.UpdateTicket(ticket);
        }
        #endregion Atualização/Criação do Ticket

        var result = new AtualizarCardAzureResponse(updateConteudo.Value.id);

        await unityOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(result);
    }
}
