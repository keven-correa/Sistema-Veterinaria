using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppSistemaVeterinaria.Data;
using WebAppSistemaVeterinaria.Data.Entities;
using WebAppSistemaVeterinaria.Helpers;
using WebAppSistemaVeterinaria.Models;

namespace WebAppSistemaVeterinaria.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClientesController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;

        public ClientesController(DataContext context, IUserHelper userHelper, ICombosHelper combosHelper, IConverterHelper converterHelper, IImageHelper imageHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
        }

        // GET: Clientes
        public IActionResult Index()
        {
            return View(_context.Clientes
                .Include(c => c.User)
                .Include(c => c.Mascotas));
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.User)
                .Include(c => c.Mascotas)
                .ThenInclude(m => m.TipoMascota)
                .Include(c => c.Mascotas)
                .ThenInclude(h => h.Historials)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Direccion = model.Direccion,
                    Cedula = model.Cedula,
                    Email = model.UsuarioNombre,
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    PhoneNumber = model.Telefono,
                    UserName = model.UsuarioNombre
                };

                var response = await _userHelper.AddUserAsync(user, model.Contraseña);
                if (response.Succeeded)
                {
                    var userInDb = await _userHelper.GetUserByEmailAsync(model.UsuarioNombre);
                    await _userHelper.AddUserToRoleAsync(userInDb, "Customer");

                    var cliente = new Cliente
                    {
                        Agendas = new List<Agenda>(),
                        Mascotas = new List<Mascota>(),
                        User = userInDb
                    };
                    _context.Clientes.Add(cliente);
                    try
                    {
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {

                        ModelState.AddModelError(string.Empty, ex.ToString());
                        return View(model);
                    }



                }
                ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);

            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Clientes
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (owner == null)
            {
                return NotFound();
            }

            var view = new EditUserViewModel
            {
                Direccion = owner.User.Direccion,
                Cedula = owner.User.Cedula,
                Nombre = owner.User.Nombre,
                Id = owner.Id,
                Apellido = owner.User.Apellido,
                Telefono = owner.User.PhoneNumber
            };

            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var owner = await _context.Clientes
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.Id == model.Id);

                owner.User.Cedula = model.Cedula;
                owner.User.Nombre = model.Nombre;
                owner.User.Apellido = model.Apellido;
                owner.User.Direccion = model.Direccion;
                owner.User.PhoneNumber = model.Telefono;

                await _userHelper.UpdateUserAsync(owner.User);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }



        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(u => u.User)
                .Include(c => c.Mascotas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            if (cliente.Mascotas.Count > 0)
            {
                ModelState.AddModelError(string.Empty, "Error");

                return RedirectToAction(nameof(Index));
            }

            await _userHelper.DeleteUserAsync(cliente.User.Email);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //// POST: Clientes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var cliente = await _context.Clientes.FindAsync(id);
        //    _context.Clientes.Remove(cliente);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AddPet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id.Value);
            if (cliente == null)
            {
                return NotFound();
            }

            var model = new MascotaViewModel
            {
                Nacimiento = DateTime.Today,
                ClienteId = cliente.Id,
                TipoMascotas = _combosHelper.GetComboTipoMascotas()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPet(MascotaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile);


                }

                var mascota = await _converterHelper.ToPetAsync(model, path, true);
                _context.Mascotas.Add(mascota);
                await _context.SaveChangesAsync();
                return RedirectToAction($"Index");

            }

            model.TipoMascotas = _combosHelper.GetComboTipoMascotas();
            return View(model);
        }


        public async Task<IActionResult> EditPet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascotas = await _context.Mascotas
                .Include(m => m.Cliente)
                .Include(m => m.TipoMascota)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mascotas == null)
            {
                return NotFound();
            }



            return View(_converterHelper.ToMascotaViewModel(mascotas));
        }

        [HttpPost]
        public async Task<IActionResult> EditPet(MascotaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = model.ImageUrl;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile);


                }

                var mascota = await _converterHelper.ToPetAsync(model, path, false);
                _context.Mascotas.Update(mascota);
                await _context.SaveChangesAsync();
                return RedirectToAction($"Index");

            }
            model.TipoMascotas = _combosHelper.GetComboTipoMascotas();

            return View(model);
        }

        public async Task<IActionResult> DetailsPet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas
                .Include(m => m.Cliente)
                .ThenInclude(o => o.User)
                .Include(m => m.Historials)
                .ThenInclude(ts => ts.TipoServicio)
                .FirstOrDefaultAsync(o => o.Id == id.Value);

            if (mascota == null)
            {
                return NotFound();
            }
            return View(mascota);
        }

        public async Task<IActionResult> AddHistory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Mascotas.FindAsync(id.Value);
            if (pet == null)
            {
                return NotFound();
            }

            var model = new HistorialViewModel
            {
                Fecha = DateTime.Now,
                MascotaId = pet.Id,
                TipoServicios = _combosHelper.GetComboTipoServicios(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddHistory(HistorialViewModel model)
        {
            if (ModelState.IsValid)
            {
                var history = await _converterHelper.ToHistorialAsync(model, true);
                _context.Historials.Add(history);
                await _context.SaveChangesAsync();
                return RedirectToAction($"Index");
            }
            model.TipoServicios = _combosHelper.GetComboTipoServicios();
            return View(model);
        }
        public async Task<IActionResult> EditHistory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var history = await _context.Historials
                .Include(h => h.Mascota)
                .Include(h => h.TipoServicio)
                .FirstOrDefaultAsync(p => p.Id == id.Value);
            if (history == null)
            {
                return NotFound();
            }

            return View(_converterHelper.ToHistoriaViewModel(history));
        }

        [HttpPost]
        public async Task<IActionResult> EditHistory(HistorialViewModel model)
        {
            if (ModelState.IsValid)
            {
                var history = await _converterHelper.ToHistorialAsync(model, false);
                _context.Historials.Update(history);
                await _context.SaveChangesAsync();
                return RedirectToAction($"Index");
            }

            model.TipoServicios = _combosHelper.GetComboTipoServicios();
            return View(model);
        }


        public async Task<IActionResult> DeleteHistory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historia = await _context.Historials
                .Include(o => o.Mascota)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (historia == null)
            {
                return NotFound();
            }

            _context.Historials.Remove(historia);
            await _context.SaveChangesAsync();
            //await _userHelper.DeleteUserAsync(owner.User.Email);
            return RedirectToAction($"{nameof(Index)}");
        }

        public async Task<IActionResult> DeletePet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas
                .Include(c => c.Cliente)
                .Include(h => h.Historials)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (mascota == null)
            {
                return NotFound();
            }
            if (mascota.Historials.Count > 0)
            {
                ModelState.AddModelError(string.Empty, "La mascota no se puede eliminar porque tiene registros en su historial.");
                return RedirectToAction($"{nameof(Index)}");
            }

            _context.Mascotas.Remove(mascota);
            await _context.SaveChangesAsync();
            //await _userHelper.DeleteUserAsync(owner.User.Email);
            return RedirectToAction($"{nameof(Index)}");
        }

    }



}

