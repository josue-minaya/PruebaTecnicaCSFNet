

using FluentValidation;
using FluentValidation.AspNetCore;
using Ventas.API.Filtros;
using Ventas.Infraestructura.Extensions;
using Serilog;
using Ventas.Infraestructura;
using Ventas.Aplicacion.Validacion;
using Ventas.Aplicacion.Validaciones;

var builder = WebApplication.CreateBuilder(args);


var origenesPermitidos = builder.Configuration
                                .GetSection("Cors:Origins")
                                .Get<string[]>()
                                 ?? Array.Empty<string>();
// Add services to the container.
SerilogExtensions.ConfigureLogger(builder.Configuration);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
})
.ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
}); builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomSwagger();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCustomCors(origenesPermitidos);
builder.Services.AddValidatorsFromAssemblyContaining<VentaDetDtoValidator>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddValidatorsFromAssemblyContaining<CrearVentaDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Host.UseSerilog();
builder.Services.AddCustomJWT(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddHttpClient("Movimientos", client =>
{
    client.BaseAddress = new Uri("https://localhost:7003/api/movimientos/");
});
builder.Services.AddHttpClient("Productos", client =>
{
    client.BaseAddress = new Uri("https://localhost:7004/api/productos/");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("Cors");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
