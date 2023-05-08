using Microsoft.AspNetCore.Identity;

namespace DataLayer.Model;

public class ApplicationRole:IdentityRole<Guid>
{
    public ApplicationRole(string name)
    {
        Name = name;
    }
}