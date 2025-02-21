using FSH.Framework.Core.Domain.Events;

namespace [Module_Namespace].[Module].Domain.Events;

public sealed record [Entity]Updated : DomainEvent
{
    public [Entity]? [Entity] { get; set; }
}
