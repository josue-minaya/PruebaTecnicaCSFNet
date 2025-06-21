using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compras.Aplicacion.DTO;
using FluentValidation;

namespace Compras.Aplicacion.Validacion
{
    public class CompraDetDtoValidator : AbstractValidator<CrearCompraDetDto>
    {
        public CompraDetDtoValidator()
        {
            RuleFor(x => x.Id_Producto)
                .NotEmpty().WithMessage("Código de producto no puede estar vacío");

            RuleFor(x => x.Cantidad)
                .GreaterThan(0)
                .WithMessage("La cantidad debe ser mayor a 0");

            RuleFor(x => x.Precio)
                .GreaterThan(0)
                .WithMessage("El precio debe ser mayor a 0");
        }
    }
}