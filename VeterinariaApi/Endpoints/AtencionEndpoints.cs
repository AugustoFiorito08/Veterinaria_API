using VeterinariaApi.Logica;
using VeterinariaApi.Logica.DTOs;

namespace VeterinariaApi.Endpoints;

public static class AtencionEndpoints
{
    public static void MapAtencionEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/atenciones", async (IAtencionLogica logica) => Results.Ok(await logica.GetAtencionesAsync()));

        app.MapGet("/atenciones/{id}", async (int id, IAtencionLogica logica) =>
        {
            var a = await logica.GetAtencionByIdAsync(id);

            return a is null ? Results.NotFound() : Results.Ok(a);
        });

        app.MapPost("/atenciones", async (AtencionCreateDto dto, IAtencionLogica logica) =>
        {
            if (string.IsNullOrWhiteSpace(dto.Motivo))
                return Results.BadRequest("Motivo es obligatorio.");

            var creado = await logica.AgregarAtencionAsync(dto);

            return Results.Created($"/atenciones/{creado.Id}", creado);
        });

        app.MapPut("/atenciones/{id}", async (int id, AtencionCreateDto dto, IAtencionLogica logica) =>
        {
            var ok = await logica.ActualizarAtencionAsync(id, dto);

            return ok ? Results.NoContent() : Results.NotFound();
        });

        app.MapDelete("/atenciones/{id}", async (int id, IAtencionLogica logica) =>
        {
            var ok = await logica.EliminarAtencionAsync(id);

            return ok ? Results.NoContent() : Results.NotFound();
        });
    }
}
