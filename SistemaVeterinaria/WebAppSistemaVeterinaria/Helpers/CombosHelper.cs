using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppSistemaVeterinaria.Data;

namespace WebAppSistemaVeterinaria.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _dataContext;

        public CombosHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IEnumerable<SelectListItem> GetComboTipoMascotas()
        {
            //var lista = new List<SelectListItem>();

            //foreach (var tipoMascota in _dataContext.TipoMascotas)
            //{
            //    //lista.Add(new SelectListItem
            //    //{
            //    //    Text = tipoMascota.Nombre,
            //    //    Value = $"{tipoMascota.Id}"
            //    //});
            //}
            var lista = _dataContext.TipoMascotas.Select(tp => new SelectListItem
            {
                Text = tp.Nombre,
                Value = $"{tp.Id}"
            })
            .OrderBy(tp => tp.Text)
            .ToList();

            lista.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un tipo de mascota...]",
                Value = "0"
            });
            return lista;


        }

        public IEnumerable<SelectListItem> GetComboTipoServicios()
        {
            var lista = _dataContext.TipoServicios.Select(tp => new SelectListItem
            {
                Text = tp.Nombre,
                Value = $"{tp.Id}"
            })
           .OrderBy(tp => tp.Text)
           .ToList();

            lista.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un tipo de servicio...]",
                Value = "0"
            });
            return lista;
        }
    }
}
