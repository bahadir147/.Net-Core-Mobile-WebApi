using Afrodit.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Afrodit.Repositories.DTOs
{
    public class UserUpdateDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Biography { get; set; }
        public string Picture { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Status Status { get; set; }
        public Int16 SharedProfile { get; set; }
    }
}
