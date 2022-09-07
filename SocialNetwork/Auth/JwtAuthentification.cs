using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi2.Auth
{
    public class JwtAuthentification
    {
        private const string KEY = "NapredneNetTehnologijeTokenZaAuthentifikaciju";
        private readonly UserManager<User> manager;

        public JwtAuthentification(UserManager<User> manager)
        {
            this.manager = manager;
        }

        public async Task<UserDto> AuthentificationAsync(String username, String password)
        {

            var user = await manager.FindByNameAsync(username);

            if(user == null) { return null;  }

            if (await manager.CheckPasswordAsync(user, password))
            {
                List<Claim> claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, user.FirstName));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserName));


                ClaimsIdentity identity = new ClaimsIdentity(claims);
                var signCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY)), SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Expires = DateTime.UtcNow.AddHours(1),
                    Subject = identity,
                    SigningCredentials = signCredentials
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
                UserDto userNew = new UserDto()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    Username=user.UserName,
                    Token = token
                };
                return userNew;
            }
            return null;
        }
    }
}
