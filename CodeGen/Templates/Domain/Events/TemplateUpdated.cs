using [Root_Namespace].Framework.Core.Domain.Events;

namespace [Root_Namespace].[Module_Namespace].[Module].Domain.Events;

public sealed record [Entity]Updated : DomainEvent
{
    public [Entity]? [Entity] { get; set; }
}
