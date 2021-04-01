using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebAppSistemaVeterinaria.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile);
    }
}