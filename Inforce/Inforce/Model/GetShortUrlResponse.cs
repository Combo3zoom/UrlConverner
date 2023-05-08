namespace Inforce.GetShortUrls;

public record ShortUrlResponse(int UrlId, string SourceUrl, string ShortUrl, DateTimeOffset CreatedAt, OwnerUser OwnerUser);