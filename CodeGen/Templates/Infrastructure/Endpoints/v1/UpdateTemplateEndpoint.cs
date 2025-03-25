using [Root_Namespace].Framework.Infrastructure.Auth.Policy;
using [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Update.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace [Root_Namespace].[Module_Namespace].[Module].Infrastructure.Endpoints.v1;

public static class Update[Entity]Endpoint
{
    internal static RouteHandlerBuilder Map[Entity]UpdateEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPut("/{[PrimaryKeyFieldNameLowerCase]:[PrimaryKeyFieldDataTypeLowerCase]}", async ([PrimaryKeyWithDataTypeLowerCase], Update[Entity]Command request, ISender mediator) =>
            {
                if ([PrimaryKeyFieldNameLowerCase] != request.[PrimaryKeyFieldNameUpperCase]) return Results.BadRequest();
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(Update[Entity]Endpoint))
            .WithSummary("update a [Entity_]")
            .WithDescription("update a [Entity_]")
            .Produces<Update[Entity]Response>()
            .RequirePermission("Permissions.[EntitySet].Update")
            .MapToApiVersion(1);
    }
}
