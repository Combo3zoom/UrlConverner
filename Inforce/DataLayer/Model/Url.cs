namespace DataLayer.Model;

public class Url
{
    public Url(int urlId, string sourceUrl, DateTimeOffset createdAt, Guid ownerUserId, User? ownerUser)
    {
        UrlId = urlId;
        SourceUrl = sourceUrl;
        CreatedAt = createdAt;
        OwnerUserId = ownerUserId;
        OwnerUser = ownerUser;
    }

    private Url() { }

    public int UrlId { get; set; }
    public string SourceUrl { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public Guid OwnerUserId { get; set; }
    public User? OwnerUser { get; set; }
}

