using System.ComponentModel;
using MediatR;

namespace [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Create.v1;

public sealed record Create[Entity]Command(
  //  [property: DefaultValue("Your default value"] string? Description = null
    ) : IRequest<Create[EntitySet]Response>;
