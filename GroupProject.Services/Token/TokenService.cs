using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GroupProject.Data;
using GroupProject.Models.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace GroupProject.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public TokenService(ApplicationDbContext context, IConfiguration configuration)
        {
            _dbContext = context;
            _configuration = configuration;
        }
        public async Task<TokenResponse> GetTokenAsync(TokenRequest request)
        {
            var adminEntity = await GetValidAdminAsync(request);
            if (adminEntity is null)
                return null;
            return GenerateToken(adminEntity);
        }
        private async Task<AdminEntity> GetValidAdminAsync(TokenRequest request)
        {
            var adminEntity = await _dbContext.Admins.FirstOrDefaultAsync(admin => admin.UserName.ToLower() == request.Username.ToLower());
            if (adminEntity is null)
                return null;
            var passwordHasher = new PasswordHasher<AdminEntity>();
            var verifyPasswordResult = passwordHasher.VerifyHashedPassword(adminEntity, adminEntity.Password, request.Password);
            if (verifyPasswordResult == PasswordVerificationResult.Failed)
                return null;

            return adminEntity;

        }
        private TokenResponse GenerateToken(AdminEntity admin)
        {
            var claims = GetClaims(admin);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(14),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenResponse = new TokenResponse
            {
                Token = tokenHandler.WriteToken(token),
                IssuedAt = token.ValidFrom,
                Expires = token.ValidTo
            };

            return tokenResponse;
        }
        private Claim[] GetClaims(AdminEntity admin)
        {
            var claims = new Claim[]
            {
                new Claim("Id", admin.Id.ToString()),
                new Claim("Username", admin.UserName)
            };
            return claims;
        }
    }
}