using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebAppSistemaVeterinaria.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboTipoMascotas();
        IEnumerable<SelectListItem> GetComboTipoServicios();
    }
}