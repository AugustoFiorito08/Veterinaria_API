namespace VeterinariaApi.Entidades;

public class Atencion
{
    public int Id { get; set; }

    public string Motivo { get; set; } = string.Empty;

    public string Tratamiento { get; set; } = string.Empty;

    public string Medicamentos { get; set; } = string.Empty;

    public DateTime Fecha { get; set; }

    public int AnimalId { get; set; }

    public Animal? Animal { get; set; }
}