﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppSistemaVeterinaria.Data.Entities
{
    public class TipoMascota
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de mascota")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; }

        public ICollection<Mascota> Mascotas { get; set; }
    }
}
