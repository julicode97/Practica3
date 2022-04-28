using Microsoft.EntityFrameworkCore;
using Practica3.Web.Models;
namespace Practica3.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }
        public DbSet<Alumno> Alumnos { get; set; }

        public DbSet<Municipio> Municipios { get; set; }

        public DbSet<Barrio> Barrios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Alumno>()
            .HasIndex(t => t.Documento)
            .IsUnique();

            modelBuilder.Entity<Municipio>()
            .HasIndex(t => t.Nombre)
            .IsUnique();

            modelBuilder.Entity<Barrio>()
            .HasIndex(t => t.Nombre)
            .IsUnique();

        }

    }
}