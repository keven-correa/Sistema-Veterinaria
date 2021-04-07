using System.Collections.Generic;

namespace VeterianariaLibraries.Models
{
    public class ClienteResponse
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Cedula { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        public ICollection<PetResponse> Pets { get; set; }
    }
}
