using CaffStore.Bll.Dtos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaffStore.Bll.Interfaces
{
    public interface IStoreService
    {
        Task Upload(IFormFile file);
        Task<IEnumerable<UploadedImagesResponseDto>> GetUploadedImages();
    }
}
