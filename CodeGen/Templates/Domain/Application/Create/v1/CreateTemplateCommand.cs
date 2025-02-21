using System.ComponentModel;
using MediatR;

namespace [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Create.v1;

public sealed record Create[Entity]Command(
  [PropertyConstructor]
    ) : IRequest<Create[Entity]Response>;
