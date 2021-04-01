using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppSistemaVeterinaria.Data;
using WebAppSistemaVeterinaria.Data.Entities;
using WebAppSistemaVeterinaria.Models;

namespace WebAppSistemaVeterinaria.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext dataContext,
            ICombosHelper combosHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
        }
        public async Task<Mascota> ToPetAsync(MascotaViewModel model, string path, bool isNew)
        {
            var mascota = new Mascota
            {
                Agendas = model.Agendas,
                Id = isNew ? 0 : model.Id,
                Nacimiento = model.Nacimiento,
                Historials = model.Historials,
                ImageUrl = path,
                Nombre = model.Nombre,
                Cliente = await _dataContext.Clientes.FindAsync(model.ClienteId),
                TipoMascota = await _dataContext.TipoMascotas.FindAsync(model.TipoMascotaId),
                Raza = model.Raza,
                Comentarios = model.Comentarios
            };

            

            return mascota;
        }
        public MascotaViewModel ToMascotaViewModel(Mascota mascota)
        {
            return new MascotaViewModel
            {
                Agendas = mascota.Agendas,
                Nacimiento = mascota.Nacimiento,
                Historials = mascota.Historials,
                ImageUrl = mascota.ImageUrl,
                Nombre = mascota.Nombre,
                Cliente = mascota.Cliente,
                TipoMascota = mascota.TipoMascota,
                Raza = mascota.Raza,
                Comentarios = mascota.Comentarios,
                Id = mascota.Id,
                ClienteId = mascota.Cliente.Id,
                TipoMascotaId = mascota.TipoMascota.Id,
                TipoMascotas = _combosHelper.GetComboTipoMascotas()
            };
        }

        public async Task<Historial> ToHistorialAsync(HistorialViewModel model, bool isNew)
        {
            return new Historial
            {
                Fecha = model.Fecha.ToUniversalTime(),
                Descripcion = model.Descripcion,
                Id = isNew ? 0 : model.Id,
                Mascota = await _dataContext.Mascotas.FindAsync(model.MascotaId),
                Comentarios = model.Comentarios,
                TipoServicio = await _dataContext.TipoServicios.FindAsync(model.TipoServicioId)
            };
        }

        public HistorialViewModel ToHistoriaViewModel(Historial historial)
        {
            return new HistorialViewModel
            {
                Fecha = historial.Fecha,
                Descripcion = historial.Descripcion,
                Id = historial.Id,
                MascotaId = historial.Mascota.Id,
                Comentarios = historial.Comentarios,
                TipoServicioId = historial.TipoServicio.Id,
                TipoServicios = _combosHelper.GetComboTipoServicios()
            };
        }
    }
}
