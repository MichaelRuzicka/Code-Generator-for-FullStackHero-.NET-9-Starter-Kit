using System.ComponentModel;
using MediatR;

namespace [Module_Namespace].[ModuleName].Application.[EntitySet].Create.v1;

public sealed record Create[Entity]Command(
  //  [property: DefaultValue("Your default value"] string? Description = null
    ) : IRequest<Create[EntitySet]Response>;
