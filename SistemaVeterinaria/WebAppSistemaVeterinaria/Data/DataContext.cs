using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppSistemaVeterinaria.Data.Entities;

namespace WebAppSistemaVeterinaria.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<Historial> Historials { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<TipoMascota> TipoMascotas { get; set; }
        public DbSet<TipoServicio> TipoServicios { get; set; }

    }
}
