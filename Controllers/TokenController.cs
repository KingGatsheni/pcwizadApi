using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _iconfiguration;
        public readonly ApplicationDbContext _dbContext;
        public TokenController(IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _iconfiguration = configuration;
            _dbContext = dbContext;
        }
        [HttpPost("auth")]
        public async Task<IActionResult> Authenticate(UserInfo userInfo)
        {
            if (userInfo != null && userInfo.UserName != null && userInfo.Email != null && userInfo.Password != null)
            {
                var user = await GetUser(userInfo.UserName, userInfo.Email, userInfo.Password);
                if(user != null){
                    var claims = new[]{
                        new Claim(JwtRegisteredClaimNames.Sub, _iconfiguration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id",user.StuffId.ToString()),
                        new Claim("Name",user.UserName.ToString()),
                        new Claim("Email", user.Email.ToString()),
                        new Claim("Password",user.Password.ToString())
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfiguration["Jwt:Key"]));
                    var signin = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        claims.ToString(),
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: signin);

                        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }else{
                    return BadRequest("Invalid Credentials");
                }
            }
            else{
                return BadRequest();
            }
        }

        // [HttpGet]
        private async Task<UserInfo> GetUser(string userName, string email, string password)
        {
            return await _dbContext.userInfos.FirstOrDefaultAsync(u => u.UserName == userName && u.Email == email && u.Password == password);
        }

    }
}