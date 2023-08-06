using MediatR;
using WebShop.WebShop.Core.Dto.UsersProfile;
using WebShop.WebShop.Core.IRepositories;

namespace WebShop.WebShop.Core.Commands.Authorization
{
    public class CreateUserAndProfileCommand:IRequest
    {
        public CreateProfileDto createProfileDto { get; set; }
    }
    public class CreateUserAndProfileHandler : IRequestHandler<CreateUserAndProfileCommand>
    {
        private readonly IAuthRepository _authRepository;
        public CreateUserAndProfileHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task<Unit> Handle(CreateUserAndProfileCommand request, CancellationToken cancellationToken)
        {
            return await _authRepository.CreateUserAndProfile(request.createProfileDto);
        }
    }
}
