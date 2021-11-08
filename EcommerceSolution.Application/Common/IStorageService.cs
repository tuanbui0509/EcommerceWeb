using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EcommerceSolution.Application.Common
{
    public interface IStorageService
    {
        string SaveFile(IFormFile file);
    }
}