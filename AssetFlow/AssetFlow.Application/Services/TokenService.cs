using AssetFlow.Application.Interfaces.IServices;
using AssetFlow.Domain.Entities;
using AssetFlow.Domain.Entities.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace AssetFlow.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            var jwtKey = config["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new ArgumentException("JWT key is missing in configuration.", nameof(config));
            }
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        }
        public string GetToken(AppUser user)
        {
            var accountId = user?.AccountId?.ToString() ?? string.Empty;
            var userId = user?.Id.ToString() ?? string.Empty;

            var claims = new List<Claim>
            {
                new Claim("urn:assetflow:usser_Id", userId),
                new Claim("urn:assetflow:account_Id", accountId),
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
