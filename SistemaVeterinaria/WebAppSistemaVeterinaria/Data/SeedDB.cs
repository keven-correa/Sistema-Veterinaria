using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppSistemaVeterinaria.Data.Entities;
using WebAppSistemaVeterinaria.Helpers;

namespace WebAppSistemaVeterinaria.Data
{
    public class SeedDB
    {
        private readonly DataContext _datacontext;
        private readonly IUserHelper _userHelper;

        public SeedDB(DataContext context, IUserHelper userHelper)
        {
            _datacontext = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _datacontext.Database.EnsureCreatedAsync();
            await checkRoles();
            await checkTiposMascotasAsync();
            await checkTipoServiciosAsync();
            var customer = await checkUserAsync("4040", "Juan", "Romero", "jromero11@gmail.com", "809 333 2222", "Calle Paraiso", "Customer");
            var manager = await checkUserAsync("1010", "Miguel", "Zuluaga", "jzuluaga55@gmail.com", "829 634 2747", "Calle Luna Calle Sol", "Admin");
            await checkClientesAsync(customer);
            await checkMascotasAsync();
            await checkAgendasAsync();
            await checkManagerAsync(manager);
            



        }

        private async Task checkManagerAsync(User user)
        {
            if (!_datacontext.Managers.Any())
            {
                _datacontext.Managers.Add(new Manager { User = user });
                await _datacontext.SaveChangesAsync();
            }
        }

        private async Task checkClientesAsync(User user)
        {
            if (!_datacontext.Clientes.Any())
            {
                _datacontext.Clientes.Add(new Cliente { User = user });
                await _datacontext.SaveChangesAsync();
            }
        }
       

        private async Task<User> checkUserAsync(string cedula, string nombre, string apellido, string email, string telefono, string direccion, string role)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Email = email,
                    UserName = email,
                    PhoneNumber = telefono,
                    Direccion = direccion,
                    Cedula = cedula
                };
                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);
            }
            return user;
        }
        private async Task checkRoles()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");
        }

        private async Task checkAgendasAsync()
        {
            if (!_datacontext.Agendas.Any())
            {
                var initialDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
                var endDate = initialDate.AddYears(1);
                
                while(initialDate < endDate)
                {
                    if (initialDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        var finalDate2 = initialDate.AddHours(10);
                        while(initialDate < finalDate2)
                        {
                            _datacontext.Agendas.Add(new Agenda
                            {
                                Fecha = initialDate.ToUniversalTime(),
                                Disponible = true
                            });
                            initialDate = initialDate.AddMinutes(30);
                        }
                        initialDate = initialDate.AddHours(14);
                    }
                    else
                    {
                        initialDate = initialDate.AddDays(1);
                    }

                    await _datacontext.SaveChangesAsync();
                }
            }


        }
        
        private async Task checkMascotasAsync()
        {
            var cliente = _datacontext.Clientes.FirstOrDefault();
            var tipoMascota = _datacontext.TipoMascotas.FirstOrDefault();
            if (!_datacontext.Mascotas.Any())
            {
                AddMascota("Otto", cliente, tipoMascota, "Chihuahua");
                AddMascota("Rollo", cliente, tipoMascota, "Rottweiller");
                await _datacontext.SaveChangesAsync();
            }
        }

        private void AddMascota(string nombre, Cliente cliente, TipoMascota tipomascota, string raza)
        {
            _datacontext.Mascotas.Add(new Mascota
            {
                Nacimiento = DateTime.Now.AddYears(-2),
                Nombre = nombre,
                Cliente = cliente,
                TipoMascota = tipomascota,
                Raza = raza
            });
        }

     
        //private void AddClientes (string cedula, string nombre, string apellido, string telefonoHogar, string celular, string direccion)
        //{
        //    _datacontext.Clientes.Add(new Cliente
        //    {
        //        Cedula = cedula,
        //        Nombre = nombre,
        //        Apellido = apellido,
        //        TelefonoFijo = telefonoHogar,
        //        Celular = celular,
        //        Direccion = direccion
        //    });
        //}

        private async Task checkTipoServiciosAsync()
        {
            if (!_datacontext.TipoServicios.Any())
            {
                _datacontext.TipoServicios.Add(new TipoServicio { Nombre = "Consulta"});
                _datacontext.TipoServicios.Add(new TipoServicio { Nombre = "Urgencia" });
                _datacontext.TipoServicios.Add(new TipoServicio { Nombre = "Vacunacion" });
                await _datacontext.SaveChangesAsync();

            }

        }

        private async Task checkTiposMascotasAsync()
        {
            if (!_datacontext.TipoMascotas.Any())
            {
                _datacontext.TipoMascotas.Add(new TipoMascota { Nombre = "Perro" });
                _datacontext.TipoMascotas.Add(new TipoMascota { Nombre = "Gato" });
                _datacontext.TipoMascotas.Add(new TipoMascota { Nombre = "Tortuga"});
                await _datacontext.SaveChangesAsync();
            }
        }
    }
}
