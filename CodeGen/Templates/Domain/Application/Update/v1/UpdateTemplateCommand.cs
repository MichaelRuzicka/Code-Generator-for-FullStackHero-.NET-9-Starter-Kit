using MediatR;

namespace [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Update.v1;

public sealed record Update[Entity]Command([PropertyConstructor]) : IRequest<Update[Entity]Response>;
