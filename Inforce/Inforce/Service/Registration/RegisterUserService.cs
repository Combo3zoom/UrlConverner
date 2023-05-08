using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using DataLayer.Model;
using DataLayer.Repository.Interface;
using Inforce.Model;
using Inforce.Service.UserService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;

namespace Inforce.Service.Registration;

public class RegisterUserService:IRegisterUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUserRepository _userRepository;
    public RegisterUserService(SignInManager<User> signInManager,
        UserManager<User> userManager, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }
    private readonly IHttpContextAccessor _httpContextAccessor;
    public string GetId()
    {
        var result = string.Empty;
        result = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return result;
    }

    public async Task Save(CancellationToken cancellationToken)
    {
        await _userRepository.Save(cancellationToken);
    }
}