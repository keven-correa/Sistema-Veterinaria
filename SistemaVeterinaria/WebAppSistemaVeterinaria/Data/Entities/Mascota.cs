using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppSistemaVeterinaria.Data.Entities
{
    public class Mascota
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; }

        [Display(Name = "Imagen")]
        public string ImageUrl { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string Raza { get; set; }

        [Display(Name = "Nacimiento")]
        [Required(ErrorMessage = "El campo {0} es oblogatorio")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Nacimiento { get; set; }

        public string Comentarios { get; set; }

        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
            ? null
            : $"https://TDB.azurewebsites.net{ImageUrl.Substring(1)}";


        [Display(Name = "nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
        public DateTime NacimientoLocal => Nacimiento.ToLocalTime();

        public TipoMascota TipoMascota { get; set; }
        public Cliente Cliente { get; set; }
        public ICollection<Historial> Historials { get; set; }
        public ICollection<Agenda> Agendas { get; set; }
    }


}
