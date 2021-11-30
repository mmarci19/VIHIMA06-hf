using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CaffStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        public async Task<ActionResult> Upload(IFormFile file)
        {
            using (var stream = System.IO.File.Create($"{ file.Name}.gif"))
            {
                await file.CopyToAsync(stream);
            }

            return Ok();
        }

        public async Task<ActionResult> Download(Guid id)
        {
            return Ok();
        }
    }
}
