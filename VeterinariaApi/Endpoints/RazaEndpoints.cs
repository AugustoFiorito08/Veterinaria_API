using VeterinariaApi.Logica;
using VeterinariaApi.Logica.DTOs;

namespace VeterinariaApi.Endpoints;

public static class RazaEndpoints
{
    public static void MapRazaEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/razas", async (IRazaLogica logica) => Results.Ok(await logica.GetRazasAsync()));

        app.MapGet("/razas/{id}", async (int id, IRazaLogica logica) =>
        {
            var r = await logica.GetRazaByIdAsync(id);

            return r is null ? Results.NotFound() : Results.Ok(r);
        });

        app.MapPost("/razas", async (RazaCreateDto dto, IRazaLogica logica) =>
        {
            if (string.IsNullOrWhiteSpace(dto.Descripcion))
                return Results.BadRequest("Descripcion es obligatoria.");

            var creado = await logica.AgregarRazaAsync(dto);

            return Results.Created($"/razas/{creado.Id}", creado);
        });

        app.MapPut("/razas/{id}", async (int id, RazaCreateDto dto, IRazaLogica logica) =>
        {
            var ok = await logica.ActualizarRazaAsync(id, dto);

            return ok ? Results.NoContent() : Results.NotFound();
        });

        app.MapDelete("/razas/{id}", async (int id, IRazaLogica logica) =>
        {
            var ok = await logica.EliminarRazaAsync(id);

            return ok ? Results.NoContent() : Results.NotFound();
        });
    }
}
