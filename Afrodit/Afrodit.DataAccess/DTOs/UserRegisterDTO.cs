using System;
using System.Collections.Generic;
using System.Text;

namespace Afrodit.Repositories.DTOs
{
    public class UserRegisterDTO
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Biography { get; set; }
        public Int16 Gender { get; set; }
        public string Picture { get; set; }
      
    }
}
