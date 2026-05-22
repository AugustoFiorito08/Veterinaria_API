namespace VeterinariaApi.Logica.DTOs;

public record AtencionDto(int Id, string Motivo, string Tratamiento, string Medicamentos, DateTime Fecha, int AnimalId, string AnimalNombre);

public record AtencionCreateDto(string Motivo, string Tratamiento, string Medicamentos, DateTime Fecha, int AnimalId);
