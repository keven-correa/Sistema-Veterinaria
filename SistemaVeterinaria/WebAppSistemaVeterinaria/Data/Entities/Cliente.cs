using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppSistemaVeterinaria.Data.Entities
{

    public class Cliente
    {
        public int Id { get; set; }

        public User User { get; set; }

        public ICollection<Mascota> Mascotas { get; set; }
        public ICollection<Agenda> Agendas { get; set; }



    }
}
