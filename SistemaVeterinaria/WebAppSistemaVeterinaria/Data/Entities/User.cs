using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebAppSistemaVeterinaria.Data.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "Cedula")]
        [MaxLength(20, ErrorMessage = "El campo {0} es obligatorio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Cedula { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido")]
        [MaxLength(50, ErrorMessage = "El campo {0} es obligatorio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Apellido { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string Direccion { get; set; }

        [Display(Name = "Nombre completo")]
        public string NombreCompleto => $"{Nombre} {Apellido}";

        [Display(Name = "Nombre completo")]
        public string NombreCompletoConCedula => $"{Nombre} {Apellido} - {Cedula}";
    }
}
