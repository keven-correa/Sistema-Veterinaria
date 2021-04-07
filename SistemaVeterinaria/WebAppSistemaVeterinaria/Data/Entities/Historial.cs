using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppSistemaVeterinaria.Data.Entities
{
    public class Historial
    {
        public int Id { get; set; }

        [Display(Name = "Descricion")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obigatorio")]
        public string Descripcion { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} es obigatorio")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        public string Comentarios { get; set; }


        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime FechaLocal => Fecha.ToLocalTime();

        public TipoServicio TipoServicio { get; set; }

        public Mascota Mascota { get; set; }
    }
}
