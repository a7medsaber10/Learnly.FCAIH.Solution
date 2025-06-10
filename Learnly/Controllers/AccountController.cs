using Learnly.APIs.DTOs;
using Learnly.APIs.Errors;
using Learnly.APIs.Helpers;
using Learnly.Core.Entities;
using Learnly.Core.Entities.Identity;
using Learnly.Core.Services.Contract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Learnly.APIs.Controllers
{
    public class AccountController : BaseAPIController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAuthService authService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user == null)
            {
                return Unauthorized(new ApiResponse(401));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            var roles = await _userManager.GetRolesAsync(user);

            if (result.Succeeded is false) 
            {
                return Unauthorized(new ApiResponse(401));
            }
            else if(roles.Contains("Teacher") && !user.IsApproved)
            {
                return Unauthorized("Trainer Account is pending approval by admin.");
            }
            else
            {
                return Ok(new UserDTO()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = await _authService.CreateTokenAsync(user, _userManager, roles)
                });
            }
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDTO registerDTO)
        {
            if(CheckEmailExist(registerDTO.Email).Result.Value)
            {
                return BadRequest(new ApiValidationErroResponse() { Errors = new string[] {"This Email already exists!"}});
            }

            var user = new AppUser()
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                UserName = registerDTO.Email.Split("@")[0],
                PhoneNumber = registerDTO.PhoneNumber,
                IsApproved = registerDTO.Role == "Teacher" ? false : true,
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded is false) return BadRequest(new ApiResponse(400));

            await _userManager.AddToRoleAsync(user, registerDTO.Role);

            var userToReturn = new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager, await _userManager.GetRolesAsync(user))
            };


            var message = registerDTO.Role == "Teacher" 
                ? "Registration Successful. Teachers Require Admin Approval." 
                : "Registration Successful.";

            return Ok(new { Message = message, User = userToReturn });
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPost("approve-teacher")]
        public async Task<ActionResult> ApproveTeacher(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user == null) return NotFound("User Not Found");

            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains("Teacher")) return BadRequest("User Is not A Teacher");

            user.IsApproved = true;

            await _userManager.UpdateAsync(user);

            return Ok(new { Message = "Teacher Approved Successfully." });
        }



        #region Forget & Reset Password


        [HttpPost("forget-password")]
        public async Task<ActionResult> ForgetPassword(ForgetPasswordDTO forgetPasswordDTO)
        {
            await _authService.SendPasswordResetLinkAsync(forgetPasswordDTO.Email);

            return Ok("Password Reset Link Has been sent.");
        }


        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword(ResetPasswordDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return BadRequest(new ApiResponse(400));

            string decodedToken = WebUtility.UrlDecode(dto.Token);

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, dto.NewPassword);

            if (!result.Succeeded)
                return BadRequest(result.Errors.Select(e => e.Description));

            return Ok("Password has been reset successfully.");
        }
        #endregion


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpGet("getCurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;

            var user = await _userManager.FindByEmailAsync(email);

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Token = await _authService.CreateTokenAsync(user, _userManager, roles)
            });
        }

        private async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            return Ok(await _userManager.FindByEmailAsync(email) is not null);
        }
    }
}
