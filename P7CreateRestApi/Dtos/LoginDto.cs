using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Dtos;

public class LoginDto
{
    [Required]
    public required string UserName { get; set; }
    [Required]
    public required string Password { get; set; }
}