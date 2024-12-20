using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Mango.Services.AuthAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Mango.Services.AuthAPI.Service
{

    public class JwtTokenGenerator : IJwtTokenGenerator
    {

        private readonly JwtOptions _jwtOptions;


        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;

        }

        public string GenerateToken(ApplicationUsers applicationUser ,  IList<string> roles)
        {

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, applicationUser.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id),
                new Claim(JwtRegisteredClaimNames.GivenName, applicationUser.name),
                
            };

            // Add role claims
            foreach (var role in roles)
            {
                claimList.Add(new Claim(ClaimTypes.Role, role));  // Role as a claim
            }
            
            

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}