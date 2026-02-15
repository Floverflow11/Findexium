using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Dtos;
using P7CreateRestApi.Dtos.User;

namespace P7CreateRestApi.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public UserController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<UserOutputDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Get()
    {
        var users = await _userManager.Users.ToListAsync();

        var outputDtos = users.Select(u => new UserOutputDto(u.Id, u.UserName, u.FullName, u.Email)).ToList();

        return Ok(outputDtos);
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(UserOutputDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromBody] RegisterDto registerUserDto)
    {
        var existingUser = await _userManager.FindByNameAsync(registerUserDto.UserName);

        if (existingUser != null)
        {
            return UnprocessableEntity();
        }

        var newUser = new User
        {
            UserName = registerUserDto.UserName,
            FullName = registerUserDto.FullName
        };

        var createResult = await _userManager.CreateAsync(newUser, registerUserDto.Password);

        if (!createResult.Succeeded)
        {
            return UnprocessableEntity();
        }

        var roleResult = await _userManager.AddToRoleAsync(newUser, "Admin");

        if (!roleResult.Succeeded)
        {
            return UnprocessableEntity();
        }

        var outputDto = new UserOutputDto(newUser.Id, newUser.UserName, newUser.FullName, newUser.Email);

        return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, outputDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserOutputDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(new UserOutputDto(user.Id, user.UserName, user.FullName, user.Email));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UserOutputDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(string id, UserUpdateDto userUpdateDto)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        user.FullName = userUpdateDto.FullName;
        user.Email = userUpdateDto.Email;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return UnprocessableEntity();
        }

        var outputDto = new UserOutputDto(user.Id, user.UserName, user.FullName, user.Email);

        return Ok(outputDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            return UnprocessableEntity();
        }

        return NoContent();
    }
}