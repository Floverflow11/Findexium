using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Dtos;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public LoginController(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        var user = await _userManager.FindByNameAsync(login.UserName);

        if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
        {
            return Unauthorized();
        }

        var token = _tokenService.CreateTokenAsync(user);

        return Ok(new { Token = token });
    }
}