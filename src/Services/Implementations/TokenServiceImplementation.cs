using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RestWith.NET.Configurations;

namespace RestWith.NET.Services.Implementations
{
    public class TokenServiceImplementation : ITokenService
    {
        private TokenConfiguration _configuration;
        public TokenServiceImplementation(TokenConfiguration  configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAcessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var options  = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims : claims,
                expires: DateTime.Now.AddMinutes(_configuration.Minutes),
                signingCredentials: signinCredentials
            ); 
            return new JwtSecurityTokenHandler().
            WriteToken(options).
            ToString();
        }
        public string GenerateRefreshToken()
        {
            var ramdomNumber =  new byte[32];
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(ramdomNumber);
                return Convert.ToBase64String(ramdomNumber); 
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters  = new TokenValidationParameters{
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey  = true,
                IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_configuration.Secret)),
                ValidateLifetime = false,

            };
            var tokenHandler =  new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            
            if(
                jwtSecurityToken == null || 
                !jwtSecurityToken.Header.Alg
                .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture))
            {
                throw new SecurityTokenException("Invalid Token");
            }
            return principal;
        }
    }
}