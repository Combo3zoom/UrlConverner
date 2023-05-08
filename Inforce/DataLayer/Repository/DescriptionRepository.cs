using DataLayer.Database;
using DataLayer.Model;
using DataLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repository;

public class DescriptionRepository: IDescriptionRepository
{
    private readonly InforceContext _context;

    public DescriptionRepository(InforceContext context)
    {
        _context = context;
    }
    public async Task<IList<Description?>> GetAll(CancellationToken cancellationToken = default)
    {
        return (await _context.Descriptions
            .ToListAsync(cancellationToken: cancellationToken))!;
    }

    public async Task<Description?> GetById(int id, CancellationToken cancellationToken=default)
    {
        return (await _context.Descriptions
            .SingleOrDefaultAsync(url => url.DescriptionId == id, cancellationToken: cancellationToken))!;
    }

    public async Task<Description?> Insert(Description description, CancellationToken cancellationToken=default)
    {
        await _context.Descriptions
            .AddAsync(description, cancellationToken);
        return description;
    }

    public async Task Delete(int urlId, CancellationToken cancellationToken=default)
    {
        var url = await _context.Descriptions
            .SingleOrDefaultAsync(url => url.DescriptionId == urlId, cancellationToken: cancellationToken);
        if (url != null) 
            _context.Descriptions.Remove(url);
    }

    public Task Delete(Description url, CancellationToken cancellationToken=default)
    {
        _context.Descriptions
            .Remove(url);
        return Task.CompletedTask;
    }

    public async Task Save(CancellationToken cancellationToken=default)
    {
        await _context
            .SaveChangesAsync(cancellationToken);
    }

    public async Task Update(string text)
    {
        var description = await _context.Descriptions.FirstOrDefaultAsync();
        if (description != null) 
            description.Text = text;
    }
}