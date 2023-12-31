﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using ss_clone.Data;
using WebShop.WebShop.Core.Dto.Login;
using WebShop.WebShop.Core.Dto.Response;
using WebShop.WebShop.Data.Models;
using WebShop.WebShop.Core.Dto.UsersProfile;
using MediatR;
using WebShop.WebShop.Core.Commands.Authorization;
using WebShop.WebShop.Core.Services.Authentication;
using WebShop.WebShop.Core.Services.Password.Service;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly ApiDbContext _context;
        private readonly IMediator _mediator;
        private readonly IPasswordService _passwordService;
        public LoginController(JwtAuthenticationManager jwtAuthenticationManager, ApiDbContext context, IMediator mediator, IPasswordService passwordService)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            _context = context;
            _mediator = mediator;
            _passwordService = passwordService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> CreateUserAndProfile(CreateProfileDto createProfileDto)
        {
            var command = new CreateUserAndProfileCommand { createProfileDto = createProfileDto };
            await _mediator.Send(command);
            return Ok("User and Profile created successfully!");
        }

        [AllowAnonymous]
        [HttpPost("Authorize")]
        public ActionResult<TokenResponse> AuthUser(UserLoginDto userLoginDto)
        {
            // Fetch user from the database using the provided email
            User user = _context.Users.FirstOrDefault(u => u.Email == userLoginDto.email);

            // Check if the user exists
            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            // Verify if the provided password matches the user's hashed password
            if (!_passwordService.VerifyPassword(userLoginDto.password, user.Password))
            {
                return Unauthorized(new { message = "Invalid password" });
            }

            // If the password is correct, generate a JWT token for authentication
            var key = "Yh2k7QSu418CZg5p6X3Pna9L0Miy4D3Bvt0JVr87Uc0j69Kqw5R2Nmf4FWs03Hdx";
            JwtAuthenticationManager jwtAuthenticationManager = new JwtAuthenticationManager(key, _context);
            string token = jwtAuthenticationManager.Authenticate(user.Email, userLoginDto.password);

            return Ok(new { Token = token, Message = "Authentication successful!" });
        }
    }
}
