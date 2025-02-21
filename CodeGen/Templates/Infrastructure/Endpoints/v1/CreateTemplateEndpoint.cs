using FSH.Framework.Infrastructure.Auth.Policy;
using [Module_Namespace].[Module].Application.[EntitySet].Create.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace [Module_Namespace].[Module].Infrastructure.Endpoints.v1;

public static class Create[Entity]Endpoint
{
    internal static RouteHandlerBuilder Map[Entity]CreationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/", async (Create[Entity]Command request, ISender mediator) =>
            {
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(Create[Entity]Endpoint))
            .WithSummary("creates a [Entity_]")
            .WithDescription("creates a [Entity_]")
            .Produces<Create[Entity]Response >()
            .RequirePermission("Permissions.[EntitySet].Create")
            .MapToApiVersion(1);
    }
}
