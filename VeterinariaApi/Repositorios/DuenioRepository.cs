using Microsoft.EntityFrameworkCore;
using VeterinariaApi.Datos;
using VeterinariaApi.Entidades;

namespace VeterinariaApi.Repositorios;

public interface IDuenioRepository
{
    Task<IEnumerable<Duenio>> ObtenerTodos();

    Task<Duenio?> ObtenerPorId(int id);

    Task<Duenio> Agregar(Duenio duenio);

    Task Actualizar(Duenio duenio);

    Task<bool> Eliminar(int id);
}

public class DuenioRepository : IDuenioRepository
{
    private readonly AppDbContext _db;

    public DuenioRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Duenio>> ObtenerTodos()
    {
        return await _db.Duenios.ToListAsync();
    }

    public async Task<Duenio?> ObtenerPorId(int id)
    {
        return await _db.Duenios.FindAsync(id);
    }

    public async Task<Duenio> Agregar(Duenio duenio)
    {
        _db.Duenios.Add(duenio);

        await _db.SaveChangesAsync();

        return duenio;
    }

    public async Task Actualizar(Duenio duenio)
    {
        _db.Duenios.Update(duenio);

        await _db.SaveChangesAsync();
    }

    public async Task<bool> Eliminar(int id)
    {
        var entidad = await _db.Duenios.FindAsync(id);

        if (entidad is null)
            return false;

        _db.Duenios.Remove(entidad);

        await _db.SaveChangesAsync();

        return true;
    }
}
