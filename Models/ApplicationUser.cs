using Microsoft.AspNetCore.Identity;

namespace E_zavetisce.Models;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public virtual Client? Client { get; set; }
    public virtual Employee? Employee { get; set; }
}