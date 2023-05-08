using DataLayer.Model;

namespace Inforce.Service.UserService;

public interface IUserService
{
    public Guid GetId();
    public Task<User> GetById(Guid userId, CancellationToken cancellationToken);
    public Task<User> GetByName(string name, CancellationToken cancellationToken);
    
}