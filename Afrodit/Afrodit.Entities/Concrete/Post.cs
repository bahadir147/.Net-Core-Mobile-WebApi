using Afrodit.Core.Entities;
using Afrodit.Core.Enums;
using System;

namespace Afrodit.Entities.Concrete
{
    public class Post : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public Status CommentStatus { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public DateTime? CreateDate { get; set; }
        public string IpAddress { get; set; }
        public Status Status { get; set; }
    }
}
