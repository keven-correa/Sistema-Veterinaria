using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAppSistemaVeterinaria.Data.Entities;

namespace WebAppSistemaVeterinaria.Models
{
    public class MascotaViewModel : Mascota
    {
        public int ClienteId { get; set; }

        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        [Display(Name ="Tipo de mascota")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes tener al menos una mascota")]
        public int TipoMascotaId { get; set; }


        [Display(Name ="Imagen")]
        public IFormFile ImageFile { get; set; }

        public IEnumerable<SelectListItem> TipoMascotas { get; set; }
    }
}
