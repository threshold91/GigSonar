using Microsoft.AspNetCore.Identity;

namespace GigSonarBackend.Identity;

public class ApplicationUser : IdentityUser
{
    public string? DisplayName { get; set; }
}