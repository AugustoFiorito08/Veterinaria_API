using Microsoft.EntityFrameworkCore;
using VeterinariaApi.Datos;
using VeterinariaApi.Entidades;

namespace VeterinariaApi.Repositorios;

public interface IAtencionRepository
{
    Task<IEnumerable<Atencion>> ObtenerTodos();

    Task<Atencion?> ObtenerPorId(int id);

    Task<Atencion> Agregar(Atencion atencion);

    Task Actualizar(Atencion atencion);

    Task<bool> Eliminar(int id);
}

public class AtencionRepository : IAtencionRepository
{
    private readonly AppDbContext _db;

    public AtencionRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Atencion>> ObtenerTodos()
    {
        return await _db.Atenciones
            .Include(a => a.Animal)
            .ToListAsync();
    }

    public async Task<Atencion?> ObtenerPorId(int id)
    {
        return await _db.Atenciones
            .Include(a => a.Animal)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Atencion> Agregar(Atencion atencion)
    {
        _db.Atenciones.Add(atencion);

        await _db.SaveChangesAsync();

        return atencion;
    }

    public async Task Actualizar(Atencion atencion)
    {
        _db.Atenciones.Update(atencion);

        await _db.SaveChangesAsync();
    }

    public async Task<bool> Eliminar(int id)
    {
        var entidad = await _db.Atenciones.FindAsync(id);

        if (entidad is null)
            return false;

        _db.Atenciones.Remove(entidad);

        await _db.SaveChangesAsync();

        return true;
    }
}
