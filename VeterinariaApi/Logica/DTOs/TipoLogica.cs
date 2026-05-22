using VeterinariaApi.Entidades;
using VeterinariaApi.Logica.DTOs;
using VeterinariaApi.Repositorios;

namespace VeterinariaApi.Logica;

public interface ITipoLogica
{
    Task<IEnumerable<Tipo>> GetTiposAsync();

    Task<Tipo?> GetTipoByIdAsync(int id);

    Task AgregarTipoAsync(TipoCreateDto dto);
}

public class TipoLogica : ITipoLogica
{
    private readonly ITipoRepository _tipoRepository;

    public TipoLogica(ITipoRepository tipoRepository)
    {
        _tipoRepository = tipoRepository;
    }

    public async Task<IEnumerable<Tipo>> GetTiposAsync()
    {
        return await _tipoRepository.ObtenerTodos();
    }

    public async Task<Tipo?> GetTipoByIdAsync(int id)
    {
        return await _tipoRepository.ObtenerPorId(id);
    }

    public async Task AgregarTipoAsync(TipoCreateDto dto)
    {
        var tipo = new Tipo
        {
            Descripcion = dto.Descripcion
        };

        await _tipoRepository.Agregar(tipo);
    }
}