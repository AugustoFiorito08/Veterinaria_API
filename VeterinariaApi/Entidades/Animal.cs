namespace VeterinariaApi.Entidades;

public class Animal
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public int Edad { get; set; }

    public Sexo Sexo { get; set; }

    public int TipoId { get; set; }

    public Tipo? Tipo { get; set; }

    public int RazaId { get; set; }

    public Raza? Raza { get; set; }

    public int? DuenioId { get; set; }

    public Duenio? Duenio { get; set; }

    public List<Atencion> Atenciones { get; set; } = new List<Atencion>();
}