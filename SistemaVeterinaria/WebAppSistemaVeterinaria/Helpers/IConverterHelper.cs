using System.Threading.Tasks;
using WebAppSistemaVeterinaria.Data.Entities;
using WebAppSistemaVeterinaria.Models;

namespace WebAppSistemaVeterinaria.Helpers
{
    public interface IConverterHelper
    {
        Task<Mascota> ToPetAsync(MascotaViewModel model, string path, bool isNew);
        MascotaViewModel ToMascotaViewModel(Mascota mascota);

        Task<Historial> ToHistorialAsync(HistorialViewModel model, bool isNew);
        HistorialViewModel ToHistoriaViewModel(Historial historial);
    }
}