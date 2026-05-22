using VeterinariaApi.Entidades;
using VeterinariaApi.Logica.DTOs;
using VeterinariaApi.Repositorios;

namespace VeterinariaApi.Logica;

public interface IDuenioLogica
{
    Task<IEnumerable<DuenioDto>> GetDueniosAsync();

    Task<DuenioDto?> GetDuenioByIdAsync(int id);

    Task<DuenioDto> AgregarDuenioAsync(DuenioCreateDto dto);

    Task<bool> ActualizarDuenioAsync(int id, DuenioCreateDto dto);

    Task<bool> EliminarDuenioAsync(int id);
}

public class DuenioLogica : IDuenioLogica
{
    private readonly IDuenioRepository _repo;

    public DuenioLogica(IDuenioRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<DuenioDto>> GetDueniosAsync()
    {
        var list = await _repo.ObtenerTodos();

        return list.Select(d => new DuenioDto(d.Id, d.Dni, d.Nombre, d.Apellido));
    }

    public async Task<DuenioDto?> GetDuenioByIdAsync(int id)
    {
        var d = await _repo.ObtenerPorId(id);

        if (d is null) return null;

        return new DuenioDto(d.Id, d.Dni, d.Nombre, d.Apellido);
    }

    public async Task<DuenioDto> AgregarDuenioAsync(DuenioCreateDto dto)
    {
        var entity = new Duenio { Dni = dto.Dni, Nombre = dto.Nombre, Apellido = dto.Apellido };

        var creado = await _repo.Agregar(entity);

        return new DuenioDto(creado.Id, creado.Dni, creado.Nombre, creado.Apellido);
    }

    public async Task<bool> ActualizarDuenioAsync(int id, DuenioCreateDto dto)
    {
        var existente = await _repo.ObtenerPorId(id);

        if (existente is null) return false;

        existente.Dni = dto.Dni;
        existente.Nombre = dto.Nombre;
        existente.Apellido = dto.Apellido;

        await _repo.Actualizar(existente);

        return true;
    }

    public async Task<bool> EliminarDuenioAsync(int id)
    {
        return await _repo.Eliminar(id);
    }
}
