using MediatR;

namespace [Module_Namespace].[ModuleName].Application.[EntitySet].Get.v1;

public class Get[Entity]Request : IRequest<[Entity]Response>
{
    public Guid Id { get; set; }
    public Get[Entity]Request(Guid id) => Id = id;
}
