using System;
using System.Collections.Generic;

namespace CaffStore.Dal.Models
{
    public class CaffFile
    {
        public Guid Id { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public string GifRoute { get; set; }
        public string CaffRoute { get; set; }
        public string Date { get; set; }
        public string Creator { get; set; }

        public ICollection<CiffFile> CiffFiles { get; set; }
    }
}
