using VeterinariaApi.Entidades;
using VeterinariaApi.Logica.DTOs;
using VeterinariaApi.Repositorios;

namespace VeterinariaApi.Logica;

public interface IRazaLogica
{
    Task<IEnumerable<RazaDto>> GetRazasAsync();

    Task<RazaDto?> GetRazaByIdAsync(int id);

    Task<RazaDto> AgregarRazaAsync(RazaCreateDto dto);

    Task<bool> ActualizarRazaAsync(int id, RazaCreateDto dto);

    Task<bool> EliminarRazaAsync(int id);
}

public class RazaLogica : IRazaLogica
{
    private readonly IRazaRepository _repo;

    public RazaLogica(IRazaRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<RazaDto>> GetRazasAsync()
    {
        var list = await _repo.ObtenerTodos();

        return list.Select(r => new RazaDto(r.Id, r.Descripcion));
    }

    public async Task<RazaDto?> GetRazaByIdAsync(int id)
    {
        var r = await _repo.ObtenerPorId(id);

        if (r is null) return null;

        return new RazaDto(r.Id, r.Descripcion);
    }

    public async Task<RazaDto> AgregarRazaAsync(RazaCreateDto dto)
    {
        var entity = new Raza { Descripcion = dto.Descripcion };

        var creado = await _repo.Agregar(entity);

        return new RazaDto(creado.Id, creado.Descripcion);
    }

    public async Task<bool> ActualizarRazaAsync(int id, RazaCreateDto dto)
    {
        var existente = await _repo.ObtenerPorId(id);

        if (existente is null) return false;

        existente.Descripcion = dto.Descripcion;

        await _repo.Actualizar(existente);

        return true;
    }

    public async Task<bool> EliminarRazaAsync(int id)
    {
        return await _repo.Eliminar(id);
    }
}
