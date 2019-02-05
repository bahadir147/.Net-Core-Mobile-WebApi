using Afrodit.Core.Entities;
using Afrodit.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Afrodit.Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Biography { get; set; }
        public Int16 Gender { get; set; }
        public string PhoneNumber { get; set; }
        public Status Status { get; set; }
        public string Picture { get; set; }
        public ProfileType ProfileType { get; set; }
        public DateTime? LastSeen { get; set; }
        public string LastIpAddress { get; set; }
        public Int16? PushEnabled { get; set; }
        public Int16? SharedProfile { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
