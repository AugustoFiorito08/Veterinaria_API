namespace VeterinariaApi.Logica.DTOs;

public record AnimalDto(
    int Id,
    string Nombre,
    int Edad,
    string Sexo,
    int TipoId,
    string TipoDescripcion,
    int RazaId,
    string RazaDescripcion,
    int? DuenioId,
    string? DuenioNombre
);

public record AnimalCreateDto(
    string Nombre,
    int Edad,
    string Sexo,
    int TipoId,
    int RazaId,
    int? DuenioId
);
