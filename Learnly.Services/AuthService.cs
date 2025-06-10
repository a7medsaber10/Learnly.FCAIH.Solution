using Learnly.Core.Entities;
using Learnly.Core.Entities.Identity;
using Learnly.Core.Services.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Learnly.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;

        public AuthService(IConfiguration configuration, UserManager<AppUser> userManager, IEmailService emailService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager, IList<string> roles)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.DisplayName),
                new Claim(ClaimTypes.Email, user.Email),
                //new Claim(ClaimTypes.Role, user.Role)
            };

            authClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            //var userRoles = await userManager.GetRolesAsync(user);

            //foreach(var role in userRoles)
            //{
            //    authClaims.Add(new Claim(ClaimTypes.Role, role));
            //}

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:AuthKey"] ?? string.Empty));

            var token = new JwtSecurityToken(
                audience: _configuration["JWT:ValidAudience"],
                issuer: _configuration["JWT:ValidIssuer"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"] ?? "0")),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task SendPasswordResetLinkAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return; // Optional: avoid disclosing user existence

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = UrlEncoder.Default.Encode(token);

            var resetLink = $"{_configuration["ApiBaseUrl"]}/api/reset-password?email={email}&token={encodedToken}";

            var resetEmail = new Email
            {
                Recepients = email,
                Subject = "Reset Your Password",
                Body = $"Please reset your password by clicking here: {resetLink}"
            };

            await _emailService.SendEmailAsync(resetEmail);
        }
    }
}
