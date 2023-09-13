using Azure;
using EmailLibrary;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Authentication;
using Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RoleAuth.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountsRepository _repo;
        private readonly IEmailDriver _email;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountsRepository repo,IEmailDriver email, IConfiguration configuration)
        {
            _repo = repo;
            _email = email;
            _configuration = configuration;

        }
        [Authorize(Roles="Admin")]
        [HttpGet("check-admin")]
        public async Task<ApiResponse> GetBookings()
        {
            var res = new ApiResponse();
            try
            {
                res.Data = "Hello Admin";
                res.StatusCode = 200;
                res.Message = "success";
            }
            catch (Exception ex)
            {
                res.Data = ex.Message;
                res.Message = "Could not get bookings";
                res.StatusCode = 400;
            }
            return res;
        }
        [Authorize(Roles = "User")]
        [HttpGet("check-user")]
        public async Task<ApiResponse> GetBookings2()
        {
            var res = new ApiResponse();
            try
            {
                res.Data = "Hello User";
                res.StatusCode = 200;
                res.Message = "success";
            }
            catch (Exception ex)
            {
                res.Data = ex.Message;
                res.Message = "Could not get bookings";
                res.StatusCode = 400;
            }
            return res;
        }
        [AllowAnonymous]
        [HttpPost("user-register")]
        public async Task<ApiResponse> RegisterUser([FromBody] RegisterModel user)
        {
            var res = new ApiResponse();
            try
            {
               var exists = await _repo.VerifyUser(user);
                if (exists==1)
                {
                    res.Data = null;
                    res.StatusCode = 403;
                    res.Message = "User Already Exists";
                }
                else
                {
                    var userAdded = await _repo.RegisterUser(user);
                    if (userAdded.Status == "success")
                    {
                        var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { userAdded.Token, email = user.Email }, Request.Scheme);
                        await _email.VerifyEmail(user.Email, confirmationLink);
                        res.Data = null;
                        res.StatusCode = 200;
                        res.Message = "User Registered";
                    }
                    else
                    {
                        res.Data = null;
                        res.StatusCode = 200;
                        res.Message = "Failed to create";
                    }
                }
              
            }
            catch (Exception ex)
            {
                res.Data = ex.Message;
                res.Message = "Could not get bookings";
                res.StatusCode = 400;
            }
            return res;
        }
        [AllowAnonymous]
        [HttpPost("admin-register")]
        public async Task<ApiResponse> RegisterAdmin([FromBody] RegisterModel user)
        {
            var res = new ApiResponse();
            try
            {
                var exists = await _repo.VerifyUser(user);
                if (exists == 1)
                {
                    res.Data = null;
                    res.StatusCode = 403;
                    res.Message = "User Already Exists";
                }
                var userAdded = await _repo.RegisterAdmin(user);
                if (userAdded.Status == "success")
                {
                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { userAdded.Token, email = user.Email }, Request.Scheme);
                    await _email.VerifyEmail(user.Email, confirmationLink);
                    res.Data = null;
                    res.StatusCode = 200;
                    res.Message = "User Registered";
                }
                else
                {
                    res.Data = null;
                    res.StatusCode = 200;
                    res.Message = "Failed to create";
                }
            }
            catch (Exception ex)
            {
                res.Data = ex.Message;
                res.Message = "Could not get bookings";
                res.StatusCode = 400;
            }
            return res;
        }
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var verified = await _repo.ConfirmEmail(token, email);
            if (verified == 1)
            {
                    return StatusCode(StatusCodes.Status200OK,
                      new Response { Status = "Success", Message = "Email Verified Successfully" });
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response { Status = "Error", Message = "This User Doesnot exist!" });
        }


        [HttpPost("login")]
        public async Task<ApiResponse>LoginAdmin([FromBody] LoginModel user)
        {
            var res = new ApiResponse();
            try
            {
                var exists = await _repo.LoginAdmin(user);
                if (exists.status == "found")
                {
                    var authClaims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
                    foreach (var role in exists.Role)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    var jwtToken = GetToken(authClaims);
                    var refreshToken = GetRefreshToken(user.Email);
                    res.Data = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        expiration = jwtToken.ValidTo,
                        refreshToken = refreshToken.token,
                        refreshTokenExpiry = refreshToken.expiry,
                    };
                    res.StatusCode = 200;
                    res.Message = "Logged In";
                }
                else
                {
                    res.Data = null;
                    res.StatusCode = 200;
                    res.Message = "Not Found";
                }
            }
            catch (Exception ex)
            {
                res.Data = ex.Message;
                res.Message = "Internal Server Error";
                res.StatusCode = 500;
            }
            return res;
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        private RefreshToken GetRefreshToken(string user)
        {
            var randomNumber = new byte[32];
            using(var randomnumbergenerator = RandomNumberGenerator.Create())
            {
                randomnumbergenerator.GetBytes(randomNumber);
                string refreshToken = Convert.ToBase64String(randomNumber);
                DateTime expiry = DateTime.Now.AddDays(1);

                return new RefreshToken { token = refreshToken, expiry = expiry };
            }
        }
    }
}
