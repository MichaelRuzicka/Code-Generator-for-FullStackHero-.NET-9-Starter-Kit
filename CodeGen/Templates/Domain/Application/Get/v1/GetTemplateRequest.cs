using MediatR;

namespace [Root_Namespace].[Module_Namespace].[Module].Application.[EntitySet].Get.v1;

public class Get[Entity]Request : IRequest<[Entity]Response>
{
    [PrimaryKeyWithProperty]
}
