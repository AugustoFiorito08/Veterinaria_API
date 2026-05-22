using VeterinariaApi.Logica;
using VeterinariaApi.Logica.DTOs;

namespace VeterinariaApi.Endpoints;

public static class AnimalEndpoints
{
    public static void MapAnimalEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/animales", async (IAnimalLogica logica) => Results.Ok(await logica.GetAnimalesAsync()));

        app.MapGet("/animales/{id}", async (int id, IAnimalLogica logica) =>
        {
            var a = await logica.GetAnimalByIdAsync(id);

            return a is null ? Results.NotFound() : Results.Ok(a);
        });

        app.MapPost("/animales", async (AnimalCreateDto dto, IAnimalLogica logica) =>
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return Results.BadRequest("Nombre es obligatorio.");

            var creado = await logica.AgregarAnimalAsync(dto);

            return Results.Created($"/animales/{creado.Id}", creado);
        });

        app.MapPut("/animales/{id}", async (int id, AnimalCreateDto dto, IAnimalLogica logica) =>
        {
            var ok = await logica.ActualizarAnimalAsync(id, dto);

            return ok ? Results.NoContent() : Results.NotFound();
        });

        app.MapDelete("/animales/{id}", async (int id, IAnimalLogica logica) =>
        {
            var ok = await logica.EliminarAnimalAsync(id);

            return ok ? Results.NoContent() : Results.NotFound();
        });
    }
}
