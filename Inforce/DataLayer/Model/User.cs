using Microsoft.AspNetCore.Identity;

namespace DataLayer.Model;

public class User: IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset UserCreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Url> Urls { get; set; } = new List<Url>();
}