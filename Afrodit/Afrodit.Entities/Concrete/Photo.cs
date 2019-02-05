using Afrodit.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Afrodit.Entities.Concrete
{
    public class Photo : IEntity
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
