using [Root_Namespace].Framework.Core.Paging;
using [Root_Namespace].Framework.Infrastructure.Auth.Policy;
using [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Get.v1;
using [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Search.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace [Root_Namespace].[Module_Namespace].[Module].Infrastructure.Endpoints.v1;

public static class Search[EntitySet]Endpoint
{
    internal static RouteHandlerBuilder MapGet[Entity]ListEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/search", async (ISender mediator, [FromBody] Search[EntitySet]Command command) =>
            {
                var response = await mediator.Send(command);
                return Results.Ok(response);
            })
            .WithName(nameof(Search[EntitySet]Endpoint))
            .WithSummary("Gets a list of [EntitySet]")
            .WithDescription("Gets a list of [EntitySet] with pagination and filtering support")
            .Produces<PagedList<[Entity]Response>>()
            .RequirePermission("Permissions.[EntitySet].View")
            .MapToApiVersion(1);
    }
}
