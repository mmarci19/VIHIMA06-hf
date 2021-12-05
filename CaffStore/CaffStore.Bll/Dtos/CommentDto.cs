using System;

namespace CaffStore.Bll.Dtos
{
    public class CommentDto
    {
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
    }
}
