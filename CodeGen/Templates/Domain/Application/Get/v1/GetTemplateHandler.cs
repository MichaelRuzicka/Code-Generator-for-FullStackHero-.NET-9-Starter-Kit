using Microsoft.Extensions.DependencyInjection;
using [Root_Namespace].[Module_Namespace].[Module].Domain.Exceptions;
using [Root_Namespace].Framework.Core.Persistence;
using [Root_Namespace].Framework.Core.Caching;
using [Root_Namespace].[Module_Namespace].[Module].Domain;
using MediatR;
using System.Threading.Tasks;

namespace [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Get.v1;

public sealed class Get[Entity]Handler([FromKeyedServices("[ServiceKey]")] IReadRepository<[Entity]> repository, ICacheService cache) : IRequestHandler<Get[Entity]Request, [Entity]Response>
{
    public async Task<[Entity]Response> Handle(Get[Entity] Request request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await cache.GetOrSetAsync(
            $"[Entity_]:{request.Id}",
            async () =>
            {
                var[Entity_]Item = await repository.GetByIdAsync(request.Id, cancellationToken);

                if ([Entity_]Item == null) throw new[Entity]NotFoundException(request.Id);

                return new[Entity]Response(brandItem.Id, [RequestFields]);
            },
            cancellationToken: cancellationToken);
        return item!;
    }
}
