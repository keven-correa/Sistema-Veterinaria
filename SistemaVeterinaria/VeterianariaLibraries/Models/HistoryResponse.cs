using System;

namespace VeterianariaLibraries.Models
{
    public class HistoryResponse
    {
        public int Id { get; set; }

        public string TipoServicio { get; set; }

        public string Descripcion { get; set; }

        public DateTime Fecha { get; set; }

        public string Comentarios { get; set; }
    }
}
