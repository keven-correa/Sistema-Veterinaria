using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VeterianariaLibraries.Models;
using WebAppSistemaVeterinaria.Data;

namespace WebAppSistemaVeterinaria.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ClientesController : Controller
    {
        private readonly DataContext _dataContext;

        public ClientesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("GetClientesByEmail")]
        public async Task<IActionResult> GetCliente(EmailRequest emailRequest)
        {
            var owner = await _dataContext.Clientes
                .Include(o => o.User)
                .Include(o => o.Mascotas)
                .ThenInclude(p => p.TipoMascota)
                .Include(o => o.Mascotas)
                .ThenInclude(p => p.Historials)
                .ThenInclude(h => h.TipoServicio)
                .FirstOrDefaultAsync(o => o.User.UserName.ToLower() == (emailRequest.Email.ToLower()));

            var response = new ClienteResponse
            {
                Id = owner.Id,
                Nombre = owner.User.Nombre,
                Apellido = owner.User.Apellido,
                Direccion = owner.User.Direccion,
                Cedula = owner.User.Cedula,
                Email = owner.User.Email,
                Telefono = owner.User.PhoneNumber,
                Pets = owner.Mascotas.Select(p => new PetResponse
                {
                    Nacimiento = p.Nacimiento,
                    Id = p.Id,
                    ImageUrl = p.ImageFullPath,
                    Nombre = p.Nombre,
                    Raza = p.Raza,
                    Comentarios = p.Comentarios,
                    TipoMascota = p.TipoMascota.Nombre,
                    Histories = p.Historials.Select(h => new HistoryResponse
                    {
                        Fecha = h.Fecha,
                        Descripcion = h.Descripcion,
                        Id = h.Id,
                        Comentarios = h.Comentarios,
                        TipoServicio = h.TipoServicio.Nombre
                    }).ToList()
                }).ToList()
            };

            return Ok(response);
        }
    }
}

