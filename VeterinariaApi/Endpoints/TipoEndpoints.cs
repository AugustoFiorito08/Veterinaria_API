using VeterinariaApi.Logica;
using VeterinariaApi.Logica.DTOs;

namespace VeterinariaApi.Endpoints;

public static class TipoEndpoints
{
    public static void MapTipoEndpoints(this IEndpointRouteBuilder app)
    {
        // GET TODOS
        app.MapGet("/tipos", async (ITipoLogica logica) =>
        {
            var tipos = await logica.GetTiposAsync();

            return Results.Ok(tipos);
        });

        // GET POR ID
        app.MapGet("/tipos/{id}", async (
            int id,
            ITipoLogica logica) =>
        {
            var tipo = await logica.GetTipoByIdAsync(id);

            if (tipo is null)
                return Results.NotFound();

            return Results.Ok(tipo);
        });

        // POST
        app.MapPost("/tipos", async (
            TipoCreateDto dto,
            ITipoLogica logica) =>
        {
            await logica.AgregarTipoAsync(dto);

            return Results.Created("/tipos", dto);
        });
    }
}