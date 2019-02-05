using Afrodit.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Afrodit.Entities.Concrete
{
    public class Follower :IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FollowerId { get; set; }
    }
}
