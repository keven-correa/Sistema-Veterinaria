using System;
using System.Collections.Generic;

namespace VeterianariaLibraries.Models
{
    public class PetResponse
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string ImageUrl { get; set; }

        public string Raza { get; set; }

        public DateTime Nacimiento { get; set; }

        public string Comentarios { get; set; }

        public string TipoMascota { get; set; }

        public ICollection<HistoryResponse> Histories { get; set; }
    }
}
