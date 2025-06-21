using FluentValidation;
using Productos.Aplicacion.DTO;

public class CrearProductoDtoValidator : AbstractValidator<CrearProductoDto>
{
    public CrearProductoDtoValidator()
    {
        RuleFor(x => x.Nombre_producto)
            .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no debe exceder los 100 caracteres.");

        RuleFor(x => x.NroLote)
            .NotEmpty().WithMessage("El número de lote es obligatorio.")
            .MaximumLength(50).WithMessage("El número de lote no debe exceder los 50 caracteres.");

        RuleFor(x => x.Costo)
            .GreaterThan(0).WithMessage("El costo debe ser mayor que cero.");

        RuleFor(x => x.PrecioVenta)
            .GreaterThanOrEqualTo(x => x.Costo)
            .WithMessage("El precio de venta debe ser mayor o igual al costo.");
    }
}
