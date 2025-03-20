using [Root_Namespace].Framework.Core.Domain.Events;
using [Root_Namespace].[Module_Namespace].Pos.Domain;

namespace [Root_Namespace].[Module_Namespace].[Module].Domain.Events;

public sealed record [Entity]Created : DomainEvent
{
    public [Entity]? [Entity] { get; set; }
}
