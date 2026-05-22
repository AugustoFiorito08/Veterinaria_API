using VeterinariaApi.Datos;

namespace VeterinariaApi.Repositorios;

public abstract class BaseRepository
{
    protected readonly AppDbContext _db;

    public BaseRepository(AppDbContext db)
    {
        _db = db;
    }
}
