using FluentValidation;
using Mapster;

namespace [Module_Namespace].[ModuleName].Application.[EntitySet].Create.v1;

public class Create[Entity]CommandValidator : AbstractValidator<Create[Entity]Command>
{
    public Create[Entity]CommandValidator()
    {
      //  RuleFor(b => b.[PropertyName]).NotEmpty().MinimumLength(2).MaximumLength(100);
    }
}
