using DataLayer.Database;
using DataLayer.Model;
using DataLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repository;

public class UserRepository:IUserRepository
{
    private readonly InforceContext _context;

    public UserRepository(InforceContext context)
    {
        _context = context;
    }

    public async Task<IList<User?>> GetAll(CancellationToken cancellationToken = default)
    {
        return (await _context.Users
            .ToListAsync(cancellationToken: cancellationToken))!;
    }

    public async Task<User?> GetById(Guid id, CancellationToken cancellationToken=default)
    {
        return await _context.Users
            .Include(user=>user.Urls)
            .SingleOrDefaultAsync(user => user.Id == id, cancellationToken: cancellationToken);
    }
    
    public async Task<User?> GetByName(string? name, CancellationToken cancellationToken=default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(user => user.Name == name, cancellationToken);
    }

    public async Task<User?> Insert(User? user, CancellationToken cancellationToken=default)
    {
        await _context.Users
            .AddAsync(user!, cancellationToken);
        return user;
    }

    public async Task DeleteAt(Guid id, CancellationToken cancellationToken=default)
    {
        var user = await _context.Users.
            FirstOrDefaultAsync(currentUser => currentUser.Id == id, cancellationToken: cancellationToken);
        if (user != null)
            _context.Remove(user);
    }

    public Task Delete(User user, CancellationToken cancellationToken=default)
    {
        _context.Remove(user);
        return Task.CompletedTask;
    }

    public async Task Save(CancellationToken cancellationToken=default)
    {
        await _context
            .SaveChangesAsync(cancellationToken);
    }
}