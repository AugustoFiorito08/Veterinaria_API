using Microsoft.EntityFrameworkCore;
using VeterinariaApi.Datos;
using VeterinariaApi.Entidades;

namespace VeterinariaApi.Repositorios;

public interface IAnimalRepository
{
    Task<IEnumerable<Animal>> ObtenerTodos();

    Task<Animal?> ObtenerPorId(int id);

    Task<Animal> Agregar(Animal animal);

    Task Actualizar(Animal animal);

    Task<bool> Eliminar(int id);
}

public class AnimalRepository : IAnimalRepository
{
    private readonly AppDbContext _db;

    public AnimalRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Animal>> ObtenerTodos()
    {
        return await _db.Animales
            .Include(a => a.Tipo)
            .Include(a => a.Raza)
            .Include(a => a.Duenio)
            .Include(a => a.Atenciones)
            .ToListAsync();
    }

    public async Task<Animal?> ObtenerPorId(int id)
    {
        return await _db.Animales
            .Include(a => a.Tipo)
            .Include(a => a.Raza)
            .Include(a => a.Duenio)
            .Include(a => a.Atenciones)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Animal> Agregar(Animal animal)
    {
        _db.Animales.Add(animal);

        await _db.SaveChangesAsync();

        return animal;
    }

    public async Task Actualizar(Animal animal)
    {
        _db.Animales.Update(animal);

        await _db.SaveChangesAsync();
    }

    public async Task<bool> Eliminar(int id)
    {
        var entidad = await _db.Animales.FindAsync(id);

        if (entidad is null)
            return false;

        _db.Animales.Remove(entidad);

        await _db.SaveChangesAsync();

        return true;
    }
}
