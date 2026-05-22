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

        app.MapPost("/tipos", async (
            TipoCreateDto dto,
            ITipoLogica logica) =>
        {
            if (string.IsNullOrWhiteSpace(dto.Descripcion))
                return Results.BadRequest("Descripcion es obligatoria.");

            var creado = await logica.AgregarTipoAsync(dto);

            return Results.Created($"/tipos/{creado.Id}", creado);
        });
    }
}