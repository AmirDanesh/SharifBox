using JwtAuthorization;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityApi.Application.Queries
{
    public class GenerateJwtTokenQueryHandler : IRequestHandler<GenerateJwtTokenQuery, string>
    {
        private readonly JwtSettings _jwtSettings;

        public GenerateJwtTokenQueryHandler(IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<string> Handle(GenerateJwtTokenQuery request, CancellationToken cancellationToken)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, request.User.Id.ToString()),
                new Claim(ClaimTypes.Name, request.User.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, request.User.Id.ToString()),
            };

            foreach (var item in request.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return await Task.Run(() => new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}