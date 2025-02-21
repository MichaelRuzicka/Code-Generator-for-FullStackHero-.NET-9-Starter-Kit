using MediatR;

namespace [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Delete.v1;

public sealed record Delete[Entity]Command(Guid Id) : IRequest;
