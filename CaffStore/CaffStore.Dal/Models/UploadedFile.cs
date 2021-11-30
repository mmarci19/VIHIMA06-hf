using System;
using System.Collections.Generic;

namespace CaffStore.Dal.Models
{
    public class UploadedFile
    {
        public Guid Id { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public string GifRoute { get; set; }
        public string CaffRoute { get; set; }
    }
}
