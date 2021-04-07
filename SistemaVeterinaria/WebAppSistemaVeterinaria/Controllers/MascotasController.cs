using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppSistemaVeterinaria.Data;
using WebAppSistemaVeterinaria.Data.Entities;
using WebAppSistemaVeterinaria.Helpers;
using WebAppSistemaVeterinaria.Models;

[Authorize(Roles = "Admin")]
public class MascotasController : Controller
{
    private readonly ICombosHelper _combosHelper;
    private readonly DataContext _dataContext;

    public MascotasController(
        ICombosHelper combosHelper,
        DataContext dataContext)
    {
        _combosHelper = combosHelper;
        _dataContext = dataContext;
    }

    public IActionResult Index()
    {
        return View(_dataContext.Mascotas
            .Include(p => p.Cliente)
            .ThenInclude(o => o.User)
            .Include(p => p.TipoMascota)
            .Include(p => p.Historials));
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pet = await _dataContext.Mascotas
            .Include(p => p.Cliente)
            .ThenInclude(o => o.User)
            .Include(p => p.Historials)
            .ThenInclude(h => h.TipoServicio)
            .FirstOrDefaultAsync(o => o.Id == id.Value);
        if (pet == null)
        {
            return NotFound();
        }

        return View(pet);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pet = await _dataContext.Mascotas
            .Include(p => p.Cliente)
            .Include(p => p.TipoMascota)
            .FirstOrDefaultAsync(p => p.Id == id.Value);
        if (pet == null)
        {
            return NotFound();
        }

        var view = new MascotaViewModel
        {
            Nacimiento = pet.Nacimiento,
            Id = pet.Id,
            ImageUrl = pet.ImageUrl,
            Nombre = pet.Nombre,
            ClienteId = pet.Cliente.Id,
            TipoMascotaId = pet.TipoMascota.Id,
            TipoMascotas = _combosHelper.GetComboTipoMascotas(),
            Raza = pet.Raza,
            Comentarios = pet.Comentarios
        };

        return View(view);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(MascotaViewModel view)
    {
        if (ModelState.IsValid)
        {
            var path = view.ImageUrl;

            if (view.ImageFile != null && view.ImageFile.Length > 0)
            {
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";

                path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot\\images\\Pets",
                    file);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await view.ImageFile.CopyToAsync(stream);
                }

                path = $"~/images/Mascotas/{file}";
            }

            var pet = new Mascota
            {
                Nacimiento = view.Nacimiento,
                Id = view.Id,
                ImageUrl = path,
                Nombre = view.Nombre,
                Cliente = await _dataContext.Clientes.FindAsync(view.ClienteId),
                TipoMascota = await _dataContext.TipoMascotas.FindAsync(view.TipoMascotaId),
                Raza = view.Raza,
                Comentarios = view.Comentarios
            };

            _dataContext.Mascotas.Update(pet);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(view);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pet = await _dataContext.Mascotas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pet == null)
        {
            return NotFound();
        }

        _dataContext.Mascotas.Remove(pet);
        await _dataContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DeleteHistory(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var history = await _dataContext.Historials
            .Include(h => h.Mascota)
            .FirstOrDefaultAsync(h => h.Id == id.Value);
        if (history == null)
        {
            return NotFound();
        }

        _dataContext.Historials.Remove(history);
        await _dataContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> EditHistory(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var history = await _dataContext.Historials
            .Include(h => h.Mascota)
            .Include(h => h.TipoServicio)
            .FirstOrDefaultAsync(p => p.Id == id.Value);
        if (history == null)
        {
            return NotFound();
        }

        var view = new HistorialViewModel
        {
            Fecha = history.Fecha,
            Descripcion = history.Descripcion,
            Id = history.Id,
            MascotaId = history.Mascota.Id,
            Comentarios = history.Comentarios,
            TipoServicioId = history.TipoServicio.Id,
            TipoServicios = _combosHelper.GetComboTipoServicios()
        };

        return View(view);
    }

    [HttpPost]
    public async Task<IActionResult> EditHistory(HistorialViewModel view)
    {
        if (ModelState.IsValid)
        {
            var history = new Historial
            {
                Fecha = view.Fecha,
                Descripcion = view.Descripcion,
                Id = view.Id,
                Mascota = await _dataContext.Mascotas.FindAsync(view.MascotaId),
                Comentarios = view.Comentarios,
                TipoServicio = await _dataContext.TipoServicios.FindAsync(view.TipoServicioId)
            };

            _dataContext.Historials.Update(history);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(view);
    }

    public async Task<IActionResult> AddHistory(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pet = await _dataContext.Mascotas.FindAsync(id.Value);
        if (pet == null)
        {
            return NotFound();
        }

        var view = new HistorialViewModel
        {
            Fecha = DateTime.Now,
            MascotaId = pet.Id,
            TipoServicios = _combosHelper.GetComboTipoServicios(),
        };

        return View(view);
    }

    [HttpPost]
    public async Task<IActionResult> AddHistory(HistorialViewModel view)
    {
        if (ModelState.IsValid)
        {
            var history = new Historial
            {
                Fecha = view.Fecha,
                Descripcion = view.Descripcion,
                Mascota = await _dataContext.Mascotas.FindAsync(view.MascotaId),
                Comentarios = view.Comentarios,
                TipoServicio = await _dataContext.TipoServicios.FindAsync(view.TipoServicioId)
            };

            _dataContext.Historials.Add(history);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(view);
    }
}

