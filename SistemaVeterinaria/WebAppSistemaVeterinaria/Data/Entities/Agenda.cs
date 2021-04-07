using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppSistemaVeterinaria.Data.Entities
{
    public class Agenda
    {
        public int Id { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyy/MM/dd H:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        public string Comentarios { get; set; }

        [Display(Name = "Esta disponible?")]
        public bool Disponible { get; set; }

        [Display(Name = "nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
        public DateTime NacimientoLocal => Fecha.ToLocalTime();

        public Cliente Cliente { get; set; }

        public Mascota Mascota { get; set; }
    }
}
