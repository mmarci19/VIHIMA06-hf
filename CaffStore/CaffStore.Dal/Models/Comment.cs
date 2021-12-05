using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffStore.Dal.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Author { get; set; }
        public DateTime CommentDate { get; set; }
        public string Content { get; set; }

        public Guid CaffFileId { get; set; }
    }
}
