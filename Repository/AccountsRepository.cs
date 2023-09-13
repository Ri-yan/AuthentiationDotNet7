using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Models.Authentication;

namespace Repository
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountsRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<RegisterResponse> RegisterUser(RegisterModel data)
        {
            IdentityUser user = new()
            {
                Email = data.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = data.Email
            };
            string Role = "User";
            if (await _roleManager.RoleExistsAsync(Role))
            {
                var result = await _userManager.CreateAsync(user, data.Password);
                if (!result.Succeeded)
                {
                    return new RegisterResponse { Token = "", Status = "error" };
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, Role);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    return new RegisterResponse { Token = token, Status = "success" };
                }

            }
            else
            {
                return new RegisterResponse { Token = "", Status = "error" };
            }
        }
        public async Task<RegisterResponse> RegisterAdmin(RegisterModel data)
        {
            IdentityUser user = new()
            {
                Email = data.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = data.Email
            };
            string Role = "Admin";
            if (await _roleManager.RoleExistsAsync(Role))
            {
                var result = await _userManager.CreateAsync(user, data.Password);
                if (!result.Succeeded)
                {
                    return new RegisterResponse { Token = "", Status = "error" };
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, Role);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    return new RegisterResponse { Token = token, Status = "success" };
                }

            }
            else
            {
                return new RegisterResponse { Token = "", Status = "error" };
            }
        }
        public async Task<int> VerifyUser(RegisterModel user)
        {
            var userExist = await _userManager.FindByEmailAsync(user.Email);
            if (userExist != null)
            {
                return 1;
            }
            return 0;
        }

        public async Task<int?> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return 1;
                }
            }
            return 0;
        }

        public async Task<LoginResponse> LoginAdmin(LoginModel login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                return new LoginResponse { status = "found", Role = userRoles };
            }
            return new LoginResponse { status = "notfound", Role = null };
        }

        public Task<LoginResponse> LoginUser(LoginModel loginModel)
        {
            throw new NotImplementedException();
        }
    }
}
