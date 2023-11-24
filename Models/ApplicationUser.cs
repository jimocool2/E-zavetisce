using Microsoft.AspNetCore.Identity;

namespace E_zavetisce.Models;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}