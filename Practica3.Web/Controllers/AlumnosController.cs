using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Practica3.Web.Data;
using Practica3.Web.Models;

namespace Practica3.Web.Controllers
{
    public class AlumnosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlumnosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Alumnos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Alumnos.Include(c => c.Municipios).ToListAsync());
        }

        // GET: Alumnos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                .Include(c => c.Municipios)
                .ThenInclude(d => d.Barrios)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // GET: Alumnos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alumnos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Alumnos.Add(alumno);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "hay un registro con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                       dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(alumno);
        }

        // GET: Alumnos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }
            return View(alumno);
        }

        // POST: Alumnos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Alumno alumno)
        {
            if (id != alumno.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alumno);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if
                   (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "hay un registro con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                       dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(alumno);
        }

        // GET: Alumnos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Alumno alumno = await _context.Alumnos
                 .Include(c => c.Municipios)
                 .ThenInclude(d => d.Barrios)
                 .FirstOrDefaultAsync(m => m.Id == id);

            if (alumno == null)
            {
                return NotFound();
            }
            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Alumnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddMunicipio(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Alumno alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }
            Municipio model = new Municipio { AlumnoId = alumno.Id };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMunicipio(Municipio municipio)
        {
            if (ModelState.IsValid)
            {
                Alumno alumno = await _context.Alumnos
                .Include(c => c.Municipios)
                .FirstOrDefaultAsync(c => c.Id == municipio.AlumnoId);
                if (alumno == null)
                {
                    return NotFound();
                }
                try
                {
                    municipio.Id = 0;
                    alumno.Municipios.Add(municipio);
                    _context.Update(alumno);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Details), new
                    {
                        Id = alumno.Id
                    });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if
                   (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                       dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(municipio);
        }

        public async Task<IActionResult> EditMunicipio(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Municipio municipio = await _context.Municipios.FindAsync(id);
            if (municipio == null)
            {
                return NotFound();
            }
            Alumno alumno = await _context.Alumnos.FirstOrDefaultAsync(c =>
           c.Municipios.FirstOrDefault(d => d.Id == municipio.Id) != null);
            municipio.AlumnoId = alumno.Id;
            return View(municipio);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMunicipio(Municipio municipio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(municipio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new
                    {
                        Id = municipio.AlumnoId
                    });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                       dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(municipio);
        }

        public async Task<IActionResult> DeleteMunicipio(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Municipio municipio = await _context.Municipios
            .Include(d => d.Barrios)
            .FirstOrDefaultAsync(m => m.Id == id);
            if (municipio == null)
            {
                return NotFound();
            }
            Alumno alumno = await _context.Alumnos.FirstOrDefaultAsync(c =>
           c.Municipios.FirstOrDefault(d => d.Id == municipio.Id) != null);
            _context.Municipios.Remove(municipio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = alumno.Id });
        }

        public async Task<IActionResult> DetailsMunicipio(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Municipio municipio = await _context.Municipios
            .Include(d => d.Barrios)
            .FirstOrDefaultAsync(m => m.Id == id);
            if (municipio == null)
            {
                return NotFound();
            }
            Alumno country = await _context.Alumnos.FirstOrDefaultAsync(c =>
           c.Municipios.FirstOrDefault(d => d.Id == municipio.Id) != null);
            municipio.AlumnoId = country.Id;
            return View(municipio);
        }

        public async Task<IActionResult> AddBarrio(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Municipio municipio = await _context.Municipios.FindAsync(id);
            if (municipio == null)
            {
                return NotFound();
            }
            Barrio model = new Barrio { MunicipioId = municipio.Id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBarrio(Barrio barrio)
        {
            if (ModelState.IsValid)
            {
                Municipio municipio = await _context.Municipios
                .Include(d => d.Barrios)
                .FirstOrDefaultAsync(c => c.Id == barrio.MunicipioId);
                if (municipio == null)
                {
                    return NotFound();
                }
                try
                {
                    barrio.Id = 0;
                    municipio.Barrios.Add(barrio);
                    _context.Update(municipio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(DetailsMunicipio), new { Id = municipio.Id });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                       dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(barrio);
        }

        public async Task<IActionResult> EditBarrio(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Barrio barrio = await _context.Barrios.FindAsync(id);
            if (barrio == null)
            {
                return NotFound();
            }
            Municipio municipio = await _context.Municipios.FirstOrDefaultAsync(d =>
           d.Barrios.FirstOrDefault(c => c.Id == barrio.Id) != null);
            barrio.MunicipioId = municipio.Id;
            return View(barrio);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBarrio(Barrio barrio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(barrio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(DetailsMunicipio), new { Id = barrio.MunicipioId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                       dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(barrio);
        }

        public async Task<IActionResult> DeleteBarrio(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Barrio barrio = await _context.Barrios
            .FirstOrDefaultAsync(m => m.Id == id);
            if (barrio == null)
            {
                return NotFound();
            }
            Municipio municipio = await _context.Municipios.FirstOrDefaultAsync(d
           => d.Barrios.FirstOrDefault(c => c.Id == barrio.Id) != null);
            _context.Barrios.Remove(barrio);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(DetailsMunicipio), new
            {
                Id = municipio.Id
            });
        }



    }
}
