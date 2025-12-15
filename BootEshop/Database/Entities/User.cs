using Microsoft.AspNetCore.Identity;

namespace BootEshop.Models.Entities;

public class User : IdentityUser<Guid>
{
    public ICollection<Order> Orders { get; set; }
}