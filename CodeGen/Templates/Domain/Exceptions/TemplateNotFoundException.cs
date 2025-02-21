using FSH.Framework.Core.Exceptions;

namespace [Module_Namespace].[Module].Domain.Exceptions;

public sealed class [Entity]NotFoundException : NotFoundException
{
    public [Entity]NotFoundException(Guid id) : base($"[Entity_] with id {id} not found")
    {
    }
}
