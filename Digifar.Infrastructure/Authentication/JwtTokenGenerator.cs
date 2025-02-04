using Digifar.API.Models.JWT;
using Digifar.Application.Common.Interfaces.Authentication;
using Digifar.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Digifar.API.Repositories.Implementation.Authentication
{
    public class JwtTokenGenerator(IOptions<JwtSettings> jwtOptions, IDateTimeProvider dateTimeProvider) : IJwtTokenGenerator
    {
        private readonly JwtSettings jwtSettings = jwtOptions.Value;
        private readonly IDateTimeProvider dateTimeProvider = dateTimeProvider;

        public string GenerateToken(string phoneNumber)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret)),

                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, phoneNumber),
                new Claim(JwtRegisteredClaimNames.PhoneNumber, phoneNumber),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: dateTimeProvider.UtcNow.AddMinutes(jwtSettings.ExpiryMinutes),
                claims: claims,
                signingCredentials: signingCredentials
                );


            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
