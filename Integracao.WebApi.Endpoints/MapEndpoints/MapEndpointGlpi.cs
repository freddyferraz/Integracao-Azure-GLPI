using Integracao.Application.Abstractions;
using Integracao.Application.UseCases.GLPIs;
using Integracao.Domain.Errors;
using Integracao.WebApi.Endpoints.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Integracao.WebApi.Endpoints.MapEndpoints;
internal sealed class MapEndpointGlpi : IEndpointMap
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("glpi/{id}", AtualizarTicketGlpiAsync)
            .Produces<AtualizarTicketGLPIResponse>(StatusCodes.Status200OK)
            .Produces(404)
            .Produces<AtualizarTicketGLPIResponse>(StatusCodes.Status400BadRequest)
            .Produces<AtualizarTicketGLPIResponse>(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithTags("Glpi")
            .MapToApiVersion(1);
    }

    public async Task<IResult> AtualizarTicketGlpiAsync
        (
            [FromRoute] long id,
            [FromBody] AtualizarTicketGLPIRequest request,
            [FromServices] ICommandHandler<AtualizarTicketGLPICommand, AtualizarTicketGLPIResponse> handler,
            CancellationToken cancellationToken
        )
    {
        var command = new AtualizarTicketGLPICommand(request.token, id, request.status, request.email, request.content);
        var result = await handler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            if(result.Error == TicketErrors.IdGLPINaoEncontrado)
            {
                return Results.NotFound(result.Error);
            }

            return Results.BadRequest(result.Error);
        }

        return Results.Ok(result.Value);
    }
}
