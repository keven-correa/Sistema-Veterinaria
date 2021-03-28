using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppSistemaVeterinaria.Data.Entities
{
    public class TipoServicio
    {
        public int Id { get; set; }

        [Display(Name ="Tipo de servicio")]
        [MaxLength(50, ErrorMessage ="El campo {0} no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public string Nombre { get; set; }

        public ICollection<Historial> Historiales { get; set; }
    }
}
