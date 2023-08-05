using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using ss_clone.Data;
using WebShop.WebShop.Core.Auth;
using WebShop.WebShop.Core.Dto.Login;
using WebShop.WebShop.Core.Dto.Response;
using WebShop.WebShop.Data.Models;
using WebShop.WebShop.Core.Dto.UsersProfile;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly ApiDbContext _context;
        public LoginController(JwtAuthenticationManager jwtAuthenticationManager, ApiDbContext context)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            _context = context;
        }

        [HttpPost("Register")]
        public IActionResult CreateUserAndProfile(CreateProfileDto createProfileDto)
        {
            var user = new User
            {
                FirstName = createProfileDto.FirstName,
                LastName = createProfileDto.LastName,
                Email = createProfileDto.Email,
                Phone = createProfileDto.Phone,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            // Hash the user's password before saving it
            string hashedPassword = HashPassword(createProfileDto.Password);
            user.Password = hashedPassword;

            var profile = new Profile
            {
                NickName = createProfileDto.NickName,
                Created = DateTime.UtcNow
            };

            // Establish the one-to-one relationship
            user.Profile = profile;
            profile.User = user;

            _context.Users.Add(user);
            _context.Profiles.Add(profile);
            _context.SaveChanges();

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
            if (!VerifyPassword(userLoginDto.password, user.Password))
            {
                return Unauthorized(new { message = "Invalid password" });
            }

            // If the password is correct, generate a JWT token for authentication
            var key = "Yh2k7QSu418CZg5p6X3Pna9L0Miy4D3Bvt0JVr87Uc0j69Kqw5R2Nmf4FWs03Hdx";
            JwtAuthenticationManager jwtAuthenticationManager = new JwtAuthenticationManager(key, _context);
            string token = jwtAuthenticationManager.Authenticate(user.Email, userLoginDto.password);

            return Ok(new { Token = token, Message = "Authentication successful!" });
        }

        // Helper method to hash the password
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Helper method to verify the password against the hashed version
        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
