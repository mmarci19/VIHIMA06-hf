using System;
using System.Collections.Generic;

namespace CaffStore.Bll.Dtos
{
    public class UploadedImagesResponseDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string GifRoute { get; set; }
        public string CaffRoute { get; set; }
    }
}
