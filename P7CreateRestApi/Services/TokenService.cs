using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Services;

public class TokenService : ITokenService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public TokenService(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string?> CreateTokenAsync(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, user.Id)
        };

        if (!string.IsNullOrEmpty(user.UserName))
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.UserName));
        }
        
        if (!string.IsNullOrEmpty(user.Email))
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        }

        var roles = await _userManager.GetRolesAsync(user);

        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var key = Encoding.UTF8.GetBytes(_configuration["Authentication:Schemes:Bearer:SigningKeys:0:Value"]!);

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(3),
            Issuer = _configuration["Authentication:Schemes:Bearer:ValidIssuer"],
            Audience = _configuration["Authentication:Schemes:Bearer:ValidAudiences:0"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var handler = new JsonWebTokenHandler();

        var token = handler.CreateToken(descriptor);

        return token;
    }
}