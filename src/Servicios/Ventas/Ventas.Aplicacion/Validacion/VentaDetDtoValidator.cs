using FluentValidation;
using Ventas.Aplicacion.DTO;

namespace Ventas.Aplicacion.Validacion;

public class VentaDetDtoValidator : AbstractValidator<CrearVentaDetDto>
{
    public VentaDetDtoValidator()
    {
        RuleFor(x => x.Id_Producto)
            .NotEmpty().WithMessage("Código de producto no puede estar vacío");

        RuleFor(x => x.Cantidad)
            .GreaterThan(0)
            .WithMessage("La cantidad debe ser mayor a 0");

        
    }
}
