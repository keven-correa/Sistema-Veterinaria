using System.ComponentModel.DataAnnotations;

namespace VeterianariaLibraries.Models
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
