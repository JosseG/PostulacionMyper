using JoseGutierrezMYPER.Models;
using Microsoft.EntityFrameworkCore;

namespace JoseGutierrezMYPER.Context;

public class TrabajadoresPruebaDbContext : DbContext
{
    
    public TrabajadoresPruebaDbContext(DbContextOptions<TrabajadoresPruebaDbContext> options):base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    public DbSet<Trabajador> Trabajadores { get; set; }
    public DbSet<Departamento> Departamentos { get; set; }
    public DbSet<Provincia> Provincias { get; set; }
    public DbSet<Distrito> Distritos { get; set; }

}