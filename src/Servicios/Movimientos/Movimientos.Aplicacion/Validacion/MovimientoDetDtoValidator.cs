using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movimientos.Aplicacion.DTO;
using FluentValidation;

namespace Movimientos.Aplicacion.Validacion
{
    public class MovimientoDetDtoValidator : AbstractValidator<CrearMovimientoDetDto>
    {
        public MovimientoDetDtoValidator()
        {
            RuleFor(x => x.Id_Producto)
                .NotEmpty().WithMessage("Código de producto no puede estar vacío");

            RuleFor(x => x.Cantidad)
                .GreaterThan(0)
                .WithMessage("La cantidad debe ser mayor a 0");

        }
    }
}