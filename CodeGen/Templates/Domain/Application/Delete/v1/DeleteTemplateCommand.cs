using MediatR;

namespace [Module_Namespace].[ModuleName].Application.[EntitySet].Delete.v1;

public sealed record DeleteBrandCommand(Guid Id) : IRequest;
