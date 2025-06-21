

using FluentValidation.AspNetCore;
using Compras.API.Filtros;
using Compras.Infraestructura.Extensions;
using Compras.Infraestructura;
using Compras.Aplicacion.Validacion;
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
}); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomSwagger();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCustomCors(origenesPermitidos);
builder.Services.AddValidatorsFromAssemblyContaining<CompraDetDtoValidator>();

builder.Services.AddValidatorsFromAssemblyContaining<CrearCompraDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();
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
