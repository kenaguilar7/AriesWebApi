using AriesWebApi.Entities.Accounts;
using AriesWebApi.Entities.Enums;
using AriesWebApi.Entities.Interfaces;
using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            //RuleSet("v", () =>
            //{
            //    RuleFor(x => x).Custom((_cuenta, context) =>
            //                    {
            //                        if (context.ParentContext.RootContextData.TryGetValue("lstCuentas", out object _value))
            //                        {
            //                            var lst = (IEnumerable<Cuenta>)_value;

            //                            var nameexist = NameExist(_cuenta, lst);

            //                            if (nameexist)
            //                            {
            //                                context.AddFailure("{PropertyNombre} de cuenta en uso");
            //                            }
            //                        }
            //                        else
            //                        {
            //                            throw new Exception("RootContextData sin información");
            //                        }

            //                    });

            //});



        }
        protected bool NameExist(Cuenta cuenta, IEnumerable<Cuenta> cuentas)
        {
            foreach (var _cuenta in cuentas)
            {
                if (_cuenta.TipoCuenta.TipoCuenta == cuenta.TipoCuenta.TipoCuenta && _cuenta.Nombre == cuenta.Nombre)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
