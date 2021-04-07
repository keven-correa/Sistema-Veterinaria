using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebAppSistemaVeterinaria.Data;
using WebAppSistemaVeterinaria.Data.Entities;
using WebAppSistemaVeterinaria.Helpers;
using WebAppSistemaVeterinaria.Models;

namespace WebAppSistemaVeterinaria.Controllers
{

    [Authorize(Roles = "Admin")]
    public class ManagersController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;


        public ManagersController(
            DataContext dataContext,
            IUserHelper userHelper)

        {
            _dataContext = dataContext;
            _userHelper = userHelper;

        }

        public IActionResult Index()
        {
            return View(_dataContext.Managers.Include(m => m.User));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _dataContext.Managers
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel view)
        {
            if (ModelState.IsValid)
            {
                var user = await AddUser(view);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Este Email esta en uso.");
                    return View(view);
                }

                var manager = new Manager { User = user };

                _dataContext.Managers.Add(manager);
                await _dataContext.SaveChangesAsync();



                return RedirectToAction(nameof(Index));
            }

            return View(view);
        }

        private async Task<User> AddUser(AddUserViewModel view)
        {
            var user = new User
            {
                Direccion = view.Direccion,
                Cedula = view.Cedula,
                Email = view.UsuarioNombre,
                Nombre = view.Nombre,
                Apellido = view.Apellido,
                PhoneNumber = view.Telefono,
                UserName = view.UsuarioNombre
            };

            var result = await _userHelper.AddUserAsync(user, view.Contraseña);
            if (result != IdentityResult.Success)
            {
                return null;
            }

            var newUser = await _userHelper.GetUserByEmailAsync(view.UsuarioNombre);
            await _userHelper.AddUserToRoleAsync(newUser, "Admin");
            return newUser;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _dataContext.Managers
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manager == null)
            {
                return NotFound();
            }

            var view = new EditUserViewModel
            {
                Direccion = manager.User.Direccion,
                Cedula = manager.User.Cedula,
                Nombre = manager.User.Nombre,
                Id = manager.Id,
                Apellido = manager.User.Apellido,
                Telefono = manager.User.PhoneNumber
            };

            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel view)
        {
            if (ModelState.IsValid)
            {
                var owner = await _dataContext.Clientes
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.Id == view.Id);

                owner.User.Cedula = view.Cedula;
                owner.User.Nombre = view.Nombre;
                owner.User.Apellido = view.Apellido;
                owner.User.Direccion = view.Direccion;
                owner.User.PhoneNumber = view.Telefono;

                await _userHelper.UpdateUserAsync(owner.User);
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

            var manager = await _dataContext.Managers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manager == null)
            {
                return NotFound();
            }

            _dataContext.Managers.Remove(manager);
            await _dataContext.SaveChangesAsync();
            await _userHelper.DeleteUserAsync(manager.User.Email);
            return RedirectToAction(nameof(Index));
        }

        private bool ManagerExists(int id)
        {
            return _dataContext.Managers.Any(e => e.Id == id);
        }
    }
}

