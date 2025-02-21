using FluentValidation;

namespace [Module_Namespace].[Module].Application.[EntitySet].Update.v1;

public class Update[Entity]CommandValidator : AbstractValidator<Update[Entity]Command>
{
    public Update[Entity]CommandValidator()
    {
       // RuleFor(b => b.Name).NotEmpty().MinimumLength(2).MaximumLength(100);
       // RuleFor(b => b.Description).MaximumLength(1000);
    }
}
