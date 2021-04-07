using System.Collections.Generic;

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
