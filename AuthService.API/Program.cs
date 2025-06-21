var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var origenesPermitidos = builder.Configuration
                                .GetSection("Cors:Origins")
                                .Get<string[]>()
                                 ?? Array.Empty<string>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors", policy =>
    {
        policy
            .WithOrigins(origenesPermitidos)
            .WithMethods("POST", "GET", "PUT")
            .WithHeaders("*");
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("Cors");
app.MapControllers();

app.Run();
