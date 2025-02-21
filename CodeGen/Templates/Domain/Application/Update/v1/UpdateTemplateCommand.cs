using MediatR;

namespace [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Update.v1;

public sealed record Update[Entity]Command(Guid Id, string? Name, string? Description = null) : IRequest<Update[Entity]Response>;
