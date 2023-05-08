using DataLayer.Model;

namespace DataLayer.Repository.Interface
{
    public interface IUserRepository: IRepository<User,Guid>
    {
        Task<User?> GetByName(string? name, CancellationToken cancellationToken = default);
    }
}