using AriesWebApi.Entities.Accounts;
using AriesWebApi.Entities.Enums;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using FluentValidation;

namespace AriesWebApi.Validators
{
    public class CuentaValidator : AbstractValidator<Cuenta>
    {
        public CuentaValidator()
        {
            RuleSet("Insert", () =>
            {
                RuleFor(x => x.Nombre)
                             .Cascade(CascadeMode.StopOnFirstFailure)
                             .NotEmpty().WithMessage("{PropertyNombre} es nulo")
                             .Length(1, 50).WithMessage("Tamaño ({TotalLength}) de {PropertyName} invalido");

                RuleFor(x => x.Detalle)
                    .MaximumLength(50).WithMessage("Tamaño ({TotalLength}) de {PropertyName} invalido");

                RuleFor(x => x.Indicador)
                             .NotEqual(IndicadorCuenta.Cuenta_Titulo)
                             .WithMessage($"No se puede crear cuentas a este nivel");
            });

            RuleSet("Update", () =>
            {
                RuleFor(cuenta => cuenta.Editable).Equal(true).WithMessage("Esta cuenta no puede ser editada"); 

                RuleFor(x => x.Nombre)
                             .Cascade(CascadeMode.StopOnFirstFailure)
                             .NotEmpty().WithMessage("{PropertyNombre} es nulo")
                             .Length(1, 50).WithMessage("Tamaño ({TotalLength}) de {PropertyName} invalido");

                RuleFor(x => x.Detalle)
                    .MaximumLength(50).WithMessage("Tamaño ({TotalLength}) de {PropertyName} invalido");

            });

        }
    }
}
