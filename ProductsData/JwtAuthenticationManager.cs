// using System;
// using System.IdentityModel.Tokens.Jwt;
// using System.Linq;
// using System.Security.Claims;
// using System.Text;
// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
// using Microsoft.IdentityModel.Tokens;
// using Models;

// namespace ProductsData
// {
//     public class JwtAuthenticationManager : IJwtAuthenticationManager
//     {
//         public readonly ApplicationDbContext _dbContext;
//         private readonly string key;
//         private IConfiguration _iconfiguration;

//         public JwtAuthenticationManager(ApplicationDbContext dbContext,IConfiguration iconiguration, string key)
//         {
//             _dbContext = dbContext;
//             _iconfiguration = iconiguration;
//             this.key = key;
//         }

//         public string Authenticate(UserInfo userInfo)
//         {
//             var user = GetUser(userInfo.UserName, userInfo.Email, userInfo.Password);
//             if (!(userInfo != null && userInfo.UserName != null && userInfo.Email != null && userInfo.Password != null))
//             {
//                 return null;
//             }

//             if (user != null)
//             {

//                 var claims = new[]{
//                         new Claim(JwtRegisteredClaimNames.Sub, _iconfiguration["Jwt:Subject"],
//                         new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
//                         new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
//                         new Claim("Id",user.StuffId.ToString()),
//                         new Claim("Name",user.UserName.ToString()),
//                         new Claim("Email", user.Email.ToString()),
//                         new Claim("Password",user.Password.ToString())
//                     };
//             }



//             var tokenHandler = new JwtSecurityTokenHandler();
//             var tokenKey = Encoding.ASCII.GetBytes(key);
//             var tokenDescriptor = new SecurityTokenDescriptor
//             {
//                 Subject = new ClaimsIdentity(new Claim[]{
//                       new Claim(ClaimTypes.Name, userInfo.UserName)

//                   }),
//                 Expires = DateTime.UtcNow.AddHours(2),
//                 SigningCredentials =
//                     new SigningCredentials(
//                         new SymmetricSecurityKey(tokenKey),
//                         SecurityAlgorithms.HmacSha256)
//             };

//             var token = tokenHandler.CreateToken(tokenDescriptor);
//             return tokenHandler.WriteToken(token);


//         }

//         public string AuthenticateAsync(UserInfo info)
//         {
//             throw new NotImplementedException();
//         }

//         private UserInfo GetUser(string userName, string email, string password)
//         {
//             return  _dbContext.userInfos.FirstOrDefault(u => u.UserName == userName && u.Email == email && u.Password == password);
//         }

//     }
// }
