using Microsoft.EntityFrameworkCore;
using EstudoMinimalAPI.Entities;

namespace EstudoMinimalAPI.DbContexts; 
public class RangoDbContext(DbContextOptions<RangoDbContext> options) : DbContext(options) {

    public DbSet<Rango> Rangos { get; set; }
    public DbSet<Ingrediente> Ingredientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) { 
        
        base.OnModelCreating(modelBuilder);

    }

}
