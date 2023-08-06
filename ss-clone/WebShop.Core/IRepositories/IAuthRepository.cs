using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebShop.WebShop.Core.Dto.UsersProfile;

namespace WebShop.WebShop.Core.IRepositories
{
    public interface IAuthRepository
    {
        public Task<Unit> CreateUserAndProfile(CreateProfileDto createProfileDto);
    }
}
