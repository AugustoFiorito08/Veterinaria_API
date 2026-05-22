namespace VeterinariaApi.Logica.DTOs;

public record TipoDto(
    int Id,
    string Descripcion
);

public record TipoCreateDto(
    string Descripcion
);