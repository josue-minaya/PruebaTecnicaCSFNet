

using FluentValidation.AspNetCore;
using Movimientos.API.Filtros;
using Movimientos.Infraestructura.Extensions;
using Movimientos.Infraestructura;
using Movimientos.Aplicacion.Validacion;
using FluentValidation;
using Serilog;

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
builder.Services.AddValidatorsFromAssemblyContaining<MovimientoDetDtoValidator>();
builder.Services.AddCustomJWT(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddValidatorsFromAssemblyContaining<CrearMovimientoDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Host.UseSerilog();

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
app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();
