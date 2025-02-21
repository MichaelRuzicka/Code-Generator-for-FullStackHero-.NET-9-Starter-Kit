using [Root_Namespace].Framework.Core.Exceptions;

namespace [Root_Namespace].[Module_Namespace].[Module].Domain.Exceptions;

public sealed class [Entity]NotFoundException : NotFoundException
{
    public [Entity]NotFoundException(Guid id) : base($"[Entity_] with id {id} not found")
    {
    }
}
