using Afrodit.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Afrodit.Repositories.DTOs
{
    public class UserPostDTO
    {
        public Post postData { get; set; }
        public List<Photo> photos { get; set; }

    }
}
