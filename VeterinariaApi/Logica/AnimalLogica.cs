using VeterinariaApi.Entidades;
using VeterinariaApi.Logica.DTOs;
using VeterinariaApi.Repositorios;

namespace VeterinariaApi.Logica;

public interface IAnimalLogica
{
    Task<IEnumerable<AnimalDto>> GetAnimalesAsync();

    Task<AnimalDto?> GetAnimalByIdAsync(int id);

    Task<AnimalDto> AgregarAnimalAsync(AnimalCreateDto dto);

    Task<bool> ActualizarAnimalAsync(int id, AnimalCreateDto dto);

    Task<bool> EliminarAnimalAsync(int id);
}

public class AnimalLogica : IAnimalLogica
{
    private readonly IAnimalRepository _repo;

    public AnimalLogica(IAnimalRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<AnimalDto>> GetAnimalesAsync()
    {
        var list = await _repo.ObtenerTodos();

        return list.Select(a => new AnimalDto(
            a.Id,
            a.Nombre,
            a.Edad,
            a.Sexo.ToString(),
            a.TipoId,
            a.Tipo?.Descripcion ?? string.Empty,
            a.RazaId,
            a.Raza?.Descripcion ?? string.Empty,
            a.DuenioId,
            a.Duenio is null ? null : $"{a.Duenio.Nombre} {a.Duenio.Apellido}"
        ));
    }

    public async Task<AnimalDto?> GetAnimalByIdAsync(int id)
    {
        var a = await _repo.ObtenerPorId(id);

        if (a is null) return null;

        return new AnimalDto(
            a.Id,
            a.Nombre,
            a.Edad,
            a.Sexo.ToString(),
            a.TipoId,
            a.Tipo?.Descripcion ?? string.Empty,
            a.RazaId,
            a.Raza?.Descripcion ?? string.Empty,
            a.DuenioId,
            a.Duenio is null ? null : $"{a.Duenio.Nombre} {a.Duenio.Apellido}"
        );
    }

    public async Task<AnimalDto> AgregarAnimalAsync(AnimalCreateDto dto)
    {
        if (!Enum.TryParse<Sexo>(dto.Sexo, true, out var sexo))
            sexo = Sexo.Macho;

        var entity = new Animal
        {
            Nombre = dto.Nombre,
            Edad = dto.Edad,
            Sexo = sexo,
            TipoId = dto.TipoId,
            RazaId = dto.RazaId,
            DuenioId = dto.DuenioId
        };

        var creado = await _repo.Agregar(entity);

        return new AnimalDto(
            creado.Id,
            creado.Nombre,
            creado.Edad,
            creado.Sexo.ToString(),
            creado.TipoId,
            creado.Tipo?.Descripcion ?? string.Empty,
            creado.RazaId,
            creado.Raza?.Descripcion ?? string.Empty,
            creado.DuenioId,
            creado.Duenio is null ? null : $"{creado.Duenio.Nombre} {creado.Duenio.Apellido}"
        );
    }

    public async Task<bool> ActualizarAnimalAsync(int id, AnimalCreateDto dto)
    {
        var existente = await _repo.ObtenerPorId(id);

        if (existente is null) return false;

        if (!Enum.TryParse<Sexo>(dto.Sexo, true, out var sexo))
            sexo = existente.Sexo;

        existente.Nombre = dto.Nombre;
        existente.Edad = dto.Edad;
        existente.Sexo = sexo;
        existente.TipoId = dto.TipoId;
        existente.RazaId = dto.RazaId;
        existente.DuenioId = dto.DuenioId;

        await _repo.Actualizar(existente);

        return true;
    }

    public async Task<bool> EliminarAnimalAsync(int id)
    {
        return await _repo.Eliminar(id);
    }
}
