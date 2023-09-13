using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Authentication
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class LoginResponse
    {
        public string? status { get; set; }
        public IList<string>? Role { get; set; }

    }
    public class RefreshToken
    {
        public string? token { get; set; }
        public DateTime? expiry { get; set; }

    }
}
