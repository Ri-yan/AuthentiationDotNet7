using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Authentication
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
    public class RegisterResponse
    {
        public string? Token { get; set; }
        public string? Status { get; set; }

    }
}
