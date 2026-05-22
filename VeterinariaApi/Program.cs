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

var app = builder.Build();

app.MapOpenApi();

app.MapScalarApiReference();

app.UseHttpsRedirection();

app.MapTipoEndpoints();

app.Run();