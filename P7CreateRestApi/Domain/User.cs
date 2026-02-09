using Microsoft.AspNetCore.Identity;

namespace P7CreateRestApi.Domain;

public class User : IdentityUser
{
    public required string FullName { get; set; }
}