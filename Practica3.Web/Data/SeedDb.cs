using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practica3.Web.Data;
using Practica3.Web.Models;
public class SeedDb
{
    private readonly ApplicationDbContext _context;
    public SeedDb(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckCountriesAsync();
    }
    private async Task CheckCountriesAsync()
    {
        if (!_context.Alumnos.Any())
        {
            _context.Alumnos.Add(new Alumno
            {
                Nombres = "Julian",
                Apellidos = "Londoño",
                Documento = "233453635",
                Direccion = "cra 50 # 40 54",
                Telefono = "3112456789",
                Correo = "julianlondono@gmail.com",
                Grado= 11,
                Edad = 21,
                Municipios = new List<Municipio>
                 {
                     new Municipio
                     {
                        Nombre = "Medellin",
                        Barrios = new List<Barrio>
                        {
                             new Barrio { Nombre = "Poblado" },
                             new Barrio { Nombre = "Aranjuez" },
                             new Barrio { Nombre = "Robleado" }
                        }

                     },
                     new Municipio
                     {
                         Nombre = "Bogota",
                         Barrios = new List<Barrio>
                         {
                             new Barrio { Nombre = "Bosa" }
                          }
                      },
                     new Municipio
                     {
                         Nombre = "Cali",
                         Barrios = new List<Barrio>
                         {
                              new Barrio { Nombre = "Pasto" },
                                 new Barrio { Nombre = "Ipiales" },
                                 new Barrio { Nombre = "Pupiales" }
                          }

                      }

                 }
            });
            _context.Alumnos.Add(new Alumno
            {
                Nombres = "Juan Fernando",
                Apellidos = "Perez",
                Documento = "135534565",
                Direccion = "cra 32 # 40 54",
                Telefono = "54654563",
                Correo = "juanfernado@gmail.com",
                Grado = 8,
                Edad = 21,
                Municipios = new List<Municipio>
                 {
                     new Municipio
                     {
                        Nombre = "Pasto",
                        Barrios = new List<Barrio>
                        {
                             new Barrio { Nombre = "Altagracia" },
                             new Barrio { Nombre = "Junin" },
                             new Barrio { Nombre = "San Jose" }
                        }

                     },
                     new Municipio
                     {
                         Nombre = "Cucuta",
                         Barrios = new List<Barrio>
                         {
                             new Barrio { Nombre = "San fernado" }
                          }
                      },
                     
                 }
            });
            await _context.SaveChangesAsync();
        }

    }
}