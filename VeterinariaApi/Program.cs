using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using VeterinariaApi.Repositorios;
using VeterinariaApi.Logica;
using VeterinariaApi.Endpoints;

using VeterinariaApi.Datos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
builder.Services.AddScoped<ITipoRepository, TipoRepository>();
builder.Services.AddScoped<ITipoLogica, TipoLogica>();

// Repositorios
builder.Services.AddScoped<IDuenioRepository, DuenioRepository>();
builder.Services.AddScoped<IRazaRepository, RazaRepository>();
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IAtencionRepository, AtencionRepository>();

// Lógica
builder.Services.AddScoped<IDuenioLogica, DuenioLogica>();
builder.Services.AddScoped<IRazaLogica, RazaLogica>();
builder.Services.AddScoped<IAnimalLogica, AnimalLogica>();
builder.Services.AddScoped<IAtencionLogica, AtencionLogica>();

var app = builder.Build();

app.MapOpenApi();

app.MapScalarApiReference();

app.UseHttpsRedirection();

app.MapTipoEndpoints();
app.MapDuenioEndpoints();
app.MapRazaEndpoints();
app.MapAnimalEndpoints();
app.MapAtencionEndpoints();

app.Run();