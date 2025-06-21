using FluentValidation;
using Ventas.Aplicacion.DTO;
using Ventas.Aplicacion.Validacion;

namespace Ventas.Aplicacion.Validaciones;

public class CrearVentaDtoValidator : AbstractValidator<CrearVentaDto>
{
    public CrearVentaDtoValidator()
    {
        RuleFor(x => x.Detalles)
            .NotEmpty().WithMessage("Debe registrar al menos un detalle")
            .ForEach(det => det.SetValidator(new VentaDetDtoValidator()));
    }
}
