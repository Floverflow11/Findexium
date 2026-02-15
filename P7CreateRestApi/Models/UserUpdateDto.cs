using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models;

public class UserUpdateDto
{
    [Required]
    public required string FullName { get; set; }
    [Required]
    public required string Email { get; set; }
}