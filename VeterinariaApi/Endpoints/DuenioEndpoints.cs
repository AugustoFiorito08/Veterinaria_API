using VeterinariaApi.Logica;
using VeterinariaApi.Logica.DTOs;

namespace VeterinariaApi.Endpoints;

public static class DuenioEndpoints
{
    public static void MapDuenioEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/duenios", async (IDuenioLogica logica) => Results.Ok(await logica.GetDueniosAsync()));

        app.MapGet("/duenios/{id}", async (int id, IDuenioLogica logica) =>
        {
            var d = await logica.GetDuenioByIdAsync(id);

            return d is null ? Results.NotFound() : Results.Ok(d);
        });

        app.MapPost("/duenios", async (DuenioCreateDto dto, IDuenioLogica logica) =>
        {
            if (string.IsNullOrWhiteSpace(dto.Dni))
                return Results.BadRequest("DNI es obligatorio.");

            var creado = await logica.AgregarDuenioAsync(dto);

            return Results.Created($"/duenios/{creado.Id}", creado);
        });

        app.MapPut("/duenios/{id}", async (int id, DuenioCreateDto dto, IDuenioLogica logica) =>
        {
            var ok = await logica.ActualizarDuenioAsync(id, dto);

            return ok ? Results.NoContent() : Results.NotFound();
        });

        app.MapDelete("/duenios/{id}", async (int id, IDuenioLogica logica) =>
        {
            var ok = await logica.EliminarDuenioAsync(id);

            return ok ? Results.NoContent() : Results.NotFound();
        });
    }
}
