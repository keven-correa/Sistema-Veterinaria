using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebAppSistemaVeterinaria.Data.Entities;

namespace WebAppSistemaVeterinaria.Models
{
    public class HistorialViewModel : Historial
    {
        public int MascotaId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Tipo de servicio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar al menos un tipo de servicio.")]
        public int TipoServicioId { get; set; }

        public IEnumerable<SelectListItem> TipoServicios { get; set; }

    }
}
