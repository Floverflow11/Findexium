using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models;

public class LoginDto
{
    [Required]
    public required string UserName { get; set; }
    [Required]
    public required string Password { get; set; }
}