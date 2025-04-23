using Microsoft.AspNetCore.Routing;

namespace Integracao.WebApi.Endpoints.Abstractions;
internal interface IEndpointMap
{
    void MapEndpoints(IEndpointRouteBuilder app);
}
