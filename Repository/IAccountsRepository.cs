using Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IAccountsRepository
    {
        public Task<int> VerifyUser(RegisterModel user);
        public Task<RegisterResponse?> RegisterUser(RegisterModel user);
        public Task<RegisterResponse> RegisterAdmin(RegisterModel user);
        public Task<int?> ConfirmEmail(string token, string email);
        public Task<LoginResponse> LoginAdmin(LoginModel loginModel);
        public Task<LoginResponse> LoginUser(LoginModel loginModel);

    }
}
