using CaffStore.Bll.Dtos;
using CaffStore.Bll.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaffStore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService service;

        public StoreController(IStoreService service)
        {
            this.service = service;
        }

        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadAsync()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();

                await service.Upload(file);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<IEnumerable<UploadedImagesResponseDto>> BrowseImages(string filter)
        {
            return await service.GetUploadedImages(filter);
        }

        [AllowAnonymous]
        [HttpGet("image")]
        public async Task<DetailsDto> GetImageById(Guid id)
        {
            return await service.GetUploadedImageById(id);
        }

        [HttpPost("comment/{imageId}")]
        public async Task AddComment(Guid imageId, [FromBody] CommentDto dto)
        {
            await service.CreateComment(imageId, dto);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("image/delete")]
        public async Task DeleteImage(Guid id)
        {
            await service.DeleteImage(id);
        }
    }
}
