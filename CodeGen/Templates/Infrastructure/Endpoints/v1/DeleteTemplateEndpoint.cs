using FSH.Framework.Infrastructure.Auth.Policy;
using [Module_Namespace].[Module].Application.[EntitySet].Delete.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace [Module_Namespace].[Module].Infrastructure.Endpoints.v1;

public static class Delete[Entity]Endpoint
{
    internal static RouteHandlerBuilder Map[Entity]DeleteEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapDelete("/{id:guid}", async (Guid id, ISender mediator) =>
             {
                 await mediator.Send(new Delete[Entity]Command(id));
                 return Results.NoContent();
             })
            .WithName(nameof(Delete[Entity]Endpoint))
            .WithSummary("deletes [Entity_] by id")
            .WithDescription("deletes [Entity_] by id")
            .Produces(StatusCodes.Status204NoContent)
            .RequirePermission("Permissions.[EntitySet].Delete")
            .MapToApiVersion(1);
    }
}
