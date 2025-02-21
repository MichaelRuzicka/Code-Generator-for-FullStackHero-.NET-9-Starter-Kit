using MediatR;

namespace [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Delete.v1;

public sealed record DeleteBrandCommand(Guid Id) : IRequest;
