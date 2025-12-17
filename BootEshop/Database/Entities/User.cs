using Microsoft.AspNetCore.Identity;

namespace Database.Entities;

public class User : IdentityUser<Guid>
{
    public ICollection<Order> Orders { get; set; }
}