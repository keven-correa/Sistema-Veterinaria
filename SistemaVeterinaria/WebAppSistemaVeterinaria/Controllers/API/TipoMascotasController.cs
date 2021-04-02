using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppSistemaVeterinaria.Data;
using WebAppSistemaVeterinaria.Data.Entities;

namespace WebAppSistemaVeterinaria.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class TipoMascotasController : ControllerBase
    {
        private readonly DataContext _context;

        public TipoMascotasController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TipoMascotas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoMascota>>> GetTipoMascotas()
        {
            return await _context.TipoMascotas.ToListAsync();
        }


    }
}
