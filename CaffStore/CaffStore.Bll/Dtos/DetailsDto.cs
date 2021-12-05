using System;
using System.Collections.Generic;

namespace CaffStore.Bll.Dtos
{
    public class DetailsDto
    {
        public Guid Id { get; set; }
        public string Date { get; set; }
        public string Creator { get; set; }
        public string FileName { get; set; }
        public string GifRoute { get; set; }
        public List<CiffDto> Ciffs { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}
