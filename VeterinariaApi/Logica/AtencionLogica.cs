using VeterinariaApi.Entidades;
using VeterinariaApi.Logica.DTOs;
using VeterinariaApi.Repositorios;

namespace VeterinariaApi.Logica;

public interface IAtencionLogica
{
    Task<IEnumerable<AtencionDto>> GetAtencionesAsync();

    Task<AtencionDto?> GetAtencionByIdAsync(int id);

    Task<AtencionDto> AgregarAtencionAsync(AtencionCreateDto dto);

    Task<bool> ActualizarAtencionAsync(int id, AtencionCreateDto dto);

    Task<bool> EliminarAtencionAsync(int id);
}

public class AtencionLogica : IAtencionLogica
{
    private readonly IAtencionRepository _repo;

    public AtencionLogica(IAtencionRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<AtencionDto>> GetAtencionesAsync()
    {
        var list = await _repo.ObtenerTodos();

        return list.Select(a => new AtencionDto(a.Id, a.Motivo, a.Tratamiento, a.Medicamentos, a.Fecha, a.AnimalId, a.Animal?.Nombre ?? string.Empty));
    }

    public async Task<AtencionDto?> GetAtencionByIdAsync(int id)
    {
        var a = await _repo.ObtenerPorId(id);

        if (a is null) return null;

        return new AtencionDto(a.Id, a.Motivo, a.Tratamiento, a.Medicamentos, a.Fecha, a.AnimalId, a.Animal?.Nombre ?? string.Empty);
    }

    public async Task<AtencionDto> AgregarAtencionAsync(AtencionCreateDto dto)
    {
        var entity = new Atencion
        {
            Motivo = dto.Motivo,
            Tratamiento = dto.Tratamiento,
            Medicamentos = dto.Medicamentos,
            Fecha = dto.Fecha,
            AnimalId = dto.AnimalId
        };

        var creado = await _repo.Agregar(entity);

        return new AtencionDto(creado.Id, creado.Motivo, creado.Tratamiento, creado.Medicamentos, creado.Fecha, creado.AnimalId, creado.Animal?.Nombre ?? string.Empty);
    }

    public async Task<bool> ActualizarAtencionAsync(int id, AtencionCreateDto dto)
    {
        var existente = await _repo.ObtenerPorId(id);

        if (existente is null) return false;

        existente.Motivo = dto.Motivo;
        existente.Tratamiento = dto.Tratamiento;
        existente.Medicamentos = dto.Medicamentos;
        existente.Fecha = dto.Fecha;
        existente.AnimalId = dto.AnimalId;

        await _repo.Actualizar(existente);

        return true;
    }

    public async Task<bool> EliminarAtencionAsync(int id)
    {
        return await _repo.Eliminar(id);
    }
}
