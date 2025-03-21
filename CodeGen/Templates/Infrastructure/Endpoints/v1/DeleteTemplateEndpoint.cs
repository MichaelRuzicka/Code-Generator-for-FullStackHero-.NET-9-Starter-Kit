using [Root_Namespace].Framework.Infrastructure.Auth.Policy;
using [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Delete.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace [Root_Namespace].[Module_Namespace].[Module].Infrastructure.Endpoints.v1;

public static class Delete[Entity]Endpoint
{
    internal static RouteHandlerBuilder Map[Entity]DeleteEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapDelete("/{[PrimaryKeyFieldNameLowerCase]:[PrimaryKeyFieldDataTypeLowerCase]}", async ([PrimaryKeyWithDataTypeUpperCase], ISender mediator) =>
             {
                 await mediator.Send(new Delete[Entity]Command([PrimaryKeyFieldNameUpperCase]));
                 return Results.NoContent();
             })
            .WithName(nameof(Delete[Entity]Endpoint))
            .WithSummary("deletes [Entity_] by [PrimaryKeyFieldNameLowerCase]")
            .WithDescription("deletes [Entity_] by [PrimaryKeyFieldNameLowerCase]")
            .Produces(StatusCodes.Status204NoContent)
            .RequirePermission("Permissions.[EntitySet].Delete")
            .MapToApiVersion(1);
    }
}
