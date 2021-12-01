using System.Collections.Generic;

namespace CaffStore.Bll.Dtos
{
    public class UploadedImagesResponseDto
    {
        public string FileName { get; set; }
        public string GifRoute { get; set; }
        public string CaffRoute { get; set; }
        public List<CiffDto> Ciffs { get; set; }
    }
}
