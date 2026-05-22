using Microsoft.EntityFrameworkCore;
using VeterinariaApi.Datos;
using VeterinariaApi.Entidades;

namespace VeterinariaApi.Repositorios;

public interface IRazaRepository
{
    Task<IEnumerable<Raza>> ObtenerTodos();

    Task<Raza?> ObtenerPorId(int id);

    Task<Raza> Agregar(Raza raza);

    Task Actualizar(Raza raza);

    Task<bool> Eliminar(int id);
}

public class RazaRepository : IRazaRepository
{
    private readonly AppDbContext _db;

    public RazaRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Raza>> ObtenerTodos()
    {
        return await _db.Razas.ToListAsync();
    }

    public async Task<Raza?> ObtenerPorId(int id)
    {
        return await _db.Razas.FindAsync(id);
    }

    public async Task<Raza> Agregar(Raza raza)
    {
        _db.Razas.Add(raza);

        await _db.SaveChangesAsync();

        return raza;
    }

    public async Task Actualizar(Raza raza)
    {
        _db.Razas.Update(raza);

        await _db.SaveChangesAsync();
    }

    public async Task<bool> Eliminar(int id)
    {
        var entidad = await _db.Razas.FindAsync(id);

        if (entidad is null)
            return false;

        _db.Razas.Remove(entidad);

        await _db.SaveChangesAsync();

        return true;
    }
}
