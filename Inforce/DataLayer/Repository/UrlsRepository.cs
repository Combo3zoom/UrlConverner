using DataLayer.Database;
using DataLayer.Model;
using DataLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repository;

public class UrlsRepository: IUrlsRepository
{
    private readonly InforceContext _context;

    public UrlsRepository(InforceContext context)
    {
        _context = context;
    }
    public async Task<IList<Url?>> GetAll(CancellationToken cancellationToken = default)
    {
        return (await _context.Urls
            .Include(url => url.OwnerUser)
            .ToListAsync(cancellationToken: cancellationToken))!;
    }

    public async Task<Url?> GetById(int id, CancellationToken cancellationToken=default)
    {
        return (await _context.Urls
            .Include(url => url.OwnerUser)
            .SingleOrDefaultAsync(url => url.UrlId == id, cancellationToken: cancellationToken))!;
    }

    public async Task<Url?> Insert(Url url, CancellationToken cancellationToken=default)
    {
        await _context.Urls
            .AddAsync(url, cancellationToken);
        return url;
    }

    public async Task Delete(int urlId, CancellationToken cancellationToken=default)
    {
        var url = await _context.Urls
            .SingleOrDefaultAsync(url => url.UrlId == urlId, cancellationToken: cancellationToken);
        if (url != null) 
            _context.Urls.Remove(url);
    }

    public Task Delete(Url url, CancellationToken cancellationToken=default)
    {
        _context.Urls
            .Remove(url);
        return Task.CompletedTask;
    }

    public async Task Save(CancellationToken cancellationToken=default)
    {
        await _context
            .SaveChangesAsync(cancellationToken);
    }
}