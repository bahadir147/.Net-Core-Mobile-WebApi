using System;
using System.Collections.Generic;
using System.Text;

namespace Afrodit.Repositories.DTOs
{
    public class UserHeadersDTO
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string UserImage { get; set; }
        public string Biography { get; set; }
        public int FollowingCount { get; set; }
        public int FollowerCount { get; set; }
        public int PostCount { get; set; }
    }
}
