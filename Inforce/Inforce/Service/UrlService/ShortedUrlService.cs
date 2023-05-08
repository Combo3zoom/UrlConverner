using DataLayer.Model;
using DataLayer.Repository.Interface;
using Inforce.GetShortUrls;
using Inforce.Model;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Inforce.Service.UrlService;

public class ShortedUrlService : IShortedUrlService
{
    private readonly IUrlsRepository _urlRepository;

    public ShortedUrlService(IUrlsRepository urlRepository)
    {
        _urlRepository = urlRepository;
    }

    public async Task<ShortUrlResponse[]> GetUrls(CancellationToken cancellationToken = default)
    {
        return (await _urlRepository.GetAll(cancellationToken))
            .Select(url => new ShortUrlResponse(url!.UrlId, 
                url.SourceUrl,
                $"https://localhost:7009/api/short-urls/{url.UrlId}/redirect",
                url.CreatedAt,
                new OwnerUser(url.OwnerUser!.Name)))
            .ToArray();
    }

    public async Task<ShortUrlResponse[]> GetMyUrls(User user, CancellationToken cancellationToken = default)
    {
        return user!.Urls
            .Select(url => new ShortUrlResponse(url.UrlId, 
                url.SourceUrl,
                $"https://localhost:7009/api/short-urls/{url.UrlId}/redirect",
                url.CreatedAt,
                new OwnerUser(user.Name)))
            .ToArray();
    }

    public async Task<Url> CreateShortUrl(CreateUrlBody createUrlBody, Guid userId, CancellationToken cancellationToken = default)
    {
        var createdUrl = new Url(0, 
            createUrlBody.Url,
            DateTimeOffset.Now, userId, null);
        
        await _urlRepository.Insert(createdUrl, cancellationToken);
        await _urlRepository.Save(cancellationToken);

        return createdUrl;
    }

    public async Task DeleteAnyShortUrl(Url url, CancellationToken cancellationToken = default)
    {
        await _urlRepository.Delete(url, cancellationToken);
        await _urlRepository.Save(cancellationToken);
    }

    public async Task DeleteOwnedShortUrl(Url url, Guid issuerUserId,
        CancellationToken cancellationToken = default)
    {
        if (url.OwnerUserId != issuerUserId)
           throw new Exception("You are not the owner of the url address");

        await _urlRepository.Delete(url, cancellationToken);
        await _urlRepository.Save(cancellationToken);
    }

    public async Task<Url> GetById(int urlId, CancellationToken cancellationToken)
    {
        var url = await _urlRepository.GetById(urlId, cancellationToken);
        if (url == null)
             throw new Exception("Url doesn't exist");
        return url;
    }
}