using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movimientos.Aplicacion.DTO;
using FluentValidation;
using Movimientos.Aplicacion.Validacion;

namespace Movimientos.Aplicacion.Validacion
{
    public class CrearMovimientoDtoValidator : AbstractValidator<CrearMovimientoDto>
    {
        public CrearMovimientoDtoValidator()
        {
            RuleFor(x => x.Detalles)
                .NotEmpty().WithMessage("Debe registrar al menos un detalle")
                .ForEach(det => det.SetValidator(new MovimientoDetDtoValidator()));
        }
    }
}