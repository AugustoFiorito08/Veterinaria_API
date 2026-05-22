using Microsoft.EntityFrameworkCore;
using VeterinariaApi.Datos;
using VeterinariaApi.Entidades;

namespace VeterinariaApi.Repositorios;

public interface ITipoRepository
{
    Task<IEnumerable<Tipo>> ObtenerTodos();

    Task<Tipo> Agregar(Tipo tipo);
}

public class TipoRepository : ITipoRepository
{
    private readonly AppDbContext _db;

    public TipoRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Tipo>> ObtenerTodos()
    {
        return await _db.Tipos.ToListAsync();
    }

    public async Task<Tipo> Agregar(Tipo tipo)
    {
        _db.Tipos.Add(tipo);

        await _db.SaveChangesAsync();

        return tipo;
    }
}