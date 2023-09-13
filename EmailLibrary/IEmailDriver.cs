using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailLibrary
{
    public interface IEmailDriver
    {
        public Task SendEmail(string reciever, string body, string subject);
        public Task PasswordReset(string email, string resetLink);
        public Task AccountVerified(string email, string resetLink);
        public Task VerifyEmail(string email, string resetLink);
    }
}
