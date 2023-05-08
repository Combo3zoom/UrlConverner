using DataLayer.Model;
using Inforce.GetShortUrls;
using Inforce.Model;

namespace Inforce.Service.UrlService;

public interface IShortedUrlService
{

    Task<ShortUrlResponse[]> GetUrls(CancellationToken cancellationToken = default);
    Task<ShortUrlResponse[]> GetMyUrls(User user, CancellationToken cancellationToken = default);
    
    Task<Url> CreateShortUrl(CreateUrlBody createUrlBody, Guid userId, CancellationToken cancellationToken = default);
    
    Task DeleteAnyShortUrl(Url url,
        CancellationToken cancellationToken = default);

    Task DeleteOwnedShortUrl(Url url, Guid issuerUserId,
        CancellationToken cancellationToken = default);

    Task<Url> GetById(int urlId, CancellationToken cancellationToken);
}