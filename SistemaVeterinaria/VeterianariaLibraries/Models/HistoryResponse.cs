using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
