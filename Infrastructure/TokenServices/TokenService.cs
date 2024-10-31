using Application.Interface.Token;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TokenServices
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        public string CreateAccessToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("user_id", user.Id.ToString()),
                new Claim("nationalNumber", user.NationalNumber),
                new Claim("role", user.Role.ToString().ToUpper())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(1000),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string CreateRandomString()
        {
            string refreshToken;
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken = Convert.ToBase64String(randomNumber);
            }
            refreshToken += Guid.NewGuid().ToString();

            return refreshToken;
        }

        public async Task<TokenResult> CreateTokenResult(User user)
        {
            var accessToken = CreateAccessToken(user);

            var tokenResult = new TokenResult
            {

                AccessToken = accessToken
            };

            return tokenResult;
        }

      
    }
}
