using System;
using System.Collections.Generic;


namespace CaffStore.Dal.Models
{
    public class CiffFile
    {
        public Guid Id { get; set; }
        public string Caption { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

        public Guid CaffFileId { get; set; }
        public CaffFile CaffFile { get; set; }
    }
}
