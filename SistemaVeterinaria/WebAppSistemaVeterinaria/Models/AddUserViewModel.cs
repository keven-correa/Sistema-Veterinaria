using System.ComponentModel.DataAnnotations;

namespace WebAppSistemaVeterinaria.Models
{
    public class AddUserViewModel : EditUserViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        [EmailAddress]
        public string UsuarioNombre { get; set; }

       
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} field must contain between {2} and {1} characters.")]
        public string Contraseña { get; set; }

        [Display(Name = "Confirmar contraseña ")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The {0} field must contain between {2} and {1} characters.")]
        [Compare("Contraseña")]
        public string Confirmarcontraseña
        {
            get; set;

        }
    }
}
