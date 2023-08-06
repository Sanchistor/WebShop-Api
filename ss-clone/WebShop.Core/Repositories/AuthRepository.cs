using MediatR;
using Microsoft.EntityFrameworkCore;
using ss_clone.Data;
using WebShop.WebShop.Core.Dto.UsersProfile;
using WebShop.WebShop.Core.IRepositories;
using WebShop.WebShop.Core.Services.Password.Service;
using WebShop.WebShop.Data.Models;

namespace WebShop.WebShop.Core.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApiDbContext _context;
        private readonly IPasswordService _passwordService;
        public AuthRepository(ApiDbContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }
        public async Task<Unit> CreateUserAndProfile(CreateProfileDto createProfileDto)
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
            string hashedPassword = _passwordService.HashPassword(createProfileDto.Password);
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
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
