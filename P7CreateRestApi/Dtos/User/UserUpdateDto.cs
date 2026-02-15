using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Dtos.User;

public class UserUpdateDto
{
    [Required]
    public required string FullName { get; set; }
    [Required]
    public required string Email { get; set; }
}