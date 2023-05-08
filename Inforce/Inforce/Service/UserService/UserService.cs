using System.Security.Claims;
using DataLayer.Model;
using DataLayer.Repository.Interface;

namespace Inforce.Service.UserService;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetId()
    {
        return new Guid(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    public async Task<User> GetById(Guid userId, CancellationToken cancellationToken)
    {
        return (await _userRepository.GetById(userId, cancellationToken))!;
    }

    public async Task<User> GetByName(string name, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByName(name, cancellationToken);
        if (user==null)
            throw new Exception("User doesn't found ");

        return user;
    }
}