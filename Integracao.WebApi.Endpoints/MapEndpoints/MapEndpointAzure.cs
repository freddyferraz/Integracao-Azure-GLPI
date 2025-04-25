using Integracao.Application.Abstractions;
using Integracao.Application.UseCases.Azures;
using Integracao.Domain.Errors;
using Integracao.WebApi.Endpoints.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;


namespace Integracao.WebApi.Endpoints.MapEndpoints;

internal sealed class MapEndpointAzure : IEndpointMap
{

    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("glpi/{id}", AtualizarAzureAsync)
            .Produces<AtualizarCardAzureResponse>(StatusCodes.Status200OK)
            .Produces(404)
            .Produces<AtualizarCardAzureResponse>(StatusCodes.Status400BadRequest)
            .Produces<AtualizarCardAzureResponse>(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithTags("Glpi")
            .MapToApiVersion(1);
    }

        public async Task<IResult> AtualizarAzureAsync
        (
            [FromRoute] long id,
            [FromBody] AtualizarCardAzureRequest request,
            [FromServices] ICommandHandler<AtualizarCardAzureCommand, AtualizarCardAzureResponse> handler,
            CancellationToken cancellationToken
        )
    {
        var command = new AtualizarCardAzureCommand(request.Area, request.Title, request.Description,
            request.Requerente, request.Observador, request.Status, request.ResponsavelAtendimento,
            request.TipoChamado, request.CaterogiraChamado, request.Prioridade, request.Impacto,
            request.Urgencia, request.Localizacao, request.Data, id);

        var result = await handler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            if (result.Error == TicketErrors.IdGLPINaoEncontrado)
            {
                return Results.NotFound(result.Error);
            }

            return Results.BadRequest(result.Error);
        }

        return Results.Ok(result.Value);
    }
}

