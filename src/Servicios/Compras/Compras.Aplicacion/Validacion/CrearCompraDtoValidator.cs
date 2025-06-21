using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compras.Aplicacion.DTO;
using FluentValidation;

namespace Compras.Aplicacion.Validacion
{
    public class CrearCompraDtoValidator : AbstractValidator<CrearCompraDto>
    {
        public CrearCompraDtoValidator()
        {
            RuleFor(x => x.Detalles)
                .NotEmpty().WithMessage("Debe registrar al menos un detalle")
                .ForEach(det => det.SetValidator(new CompraDetDtoValidator()));
        }
    }
}