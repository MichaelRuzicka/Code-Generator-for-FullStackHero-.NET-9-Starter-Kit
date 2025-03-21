using [Root_Namespace].Framework.Infrastructure.Auth.Policy;
using [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Get.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace [Root_Namespace].[Module_Namespace].[Module].Infrastructure.Endpoints.v1;

public static class Get[Entity]Endpoint
{
    internal static RouteHandlerBuilder MapGet[Entity]Endpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
     .MapGet("/{[PrimaryKeyFieldNameLowerCase]:[PrimaryKeyFieldDataTypeLowerCase]}", async ([PrimaryKeyWithDataTypeLowerCase], ISender mediator) =>
            {
                var response = await mediator.Send(new Get[Entity]Request(id));
                return Results.Ok(response);
            })
            .WithName(nameof(Get[Entity]Endpoint))
            .WithSummary("gets [Entity_] by [PrimaryKeyFieldNameLowerCase]")
            .WithDescription("gets [Entity_] by [PrimaryKeyFieldNameLowerCase]")
            .Produces<[Entity]Response >()
            .RequirePermission("Permissions.[EntitySet].View")
            .MapToApiVersion(1);
    }
}
