using Microsoft.EntityFrameworkCore;
using VeterinariaApi.Entidades;

namespace VeterinariaApi.Datos;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Animal> Animales => Set<Animal>();

    public DbSet<Duenio> Duenios => Set<Duenio>();

    public DbSet<Tipo> Tipos => Set<Tipo>();

    public DbSet<Raza> Razas => Set<Raza>();

    public DbSet<Atencion> Atenciones => Set<Atencion>();
}