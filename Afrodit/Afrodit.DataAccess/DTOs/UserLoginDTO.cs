using System;
using System.Collections.Generic;
using System.Text;

namespace Afrodit.Repositories.DTOs
{
    public class UserLoginDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
