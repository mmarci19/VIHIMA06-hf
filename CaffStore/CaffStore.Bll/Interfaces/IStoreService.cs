using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CaffStore.Bll.Interfaces
{
    public interface IStoreService
    {
        Task Upload(IFormFile file);
    }
}
