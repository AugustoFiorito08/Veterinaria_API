using VeterinariaApi.Entidades;
using VeterinariaApi.Logica.DTOs;
using VeterinariaApi.Repositorios;

namespace VeterinariaApi.Logica;

public interface ITipoLogica
{
    Task<IEnumerable<TipoDto>> GetTiposAsync();

    Task<TipoDto> AgregarTipoAsync(TipoCreateDto dto);
}

public class TipoLogica : ITipoLogica
{
    private readonly ITipoRepository _tipoRepository;

    public TipoLogica(ITipoRepository tipoRepository)
    {
        _tipoRepository = tipoRepository;
    }

    public async Task<IEnumerable<TipoDto>> GetTiposAsync()
    {
        var tipos = await _tipoRepository.ObtenerTodos();

        return tipos.Select(t => new TipoDto(t.Id, t.Descripcion));
    }

    public async Task<TipoDto> AgregarTipoAsync(TipoCreateDto dto)
    {
        var tipo = new Tipo
        {
            Descripcion = dto.Descripcion
        };

        var creado = await _tipoRepository.Agregar(tipo);

        return new TipoDto(creado.Id, creado.Descripcion);
    }
}