using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PracticeInventory.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PracticeInventory.Infrastucture.Authentication;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;
    private readonly UserManager<IdentityUser> _userManager;

    public JwtProvider(IOptions<JwtOptions> jwtOptions, UserManager<IdentityUser> userManager)
    {
        _jwtOptions = jwtOptions.Value;
        _userManager = userManager;
    }

    public async Task<string> Generate(IdentityUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var userRoles = await _userManager.GetRolesAsync(user);

        var claim = new List<Claim>
        {
            new Claim(ClaimTypes.Name, $"{user.UserName}"),
            new Claim(ClaimTypes.Email, user.Email)
        };

        foreach (var userRole in userRoles)
        {
            claim.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var token = new JwtSecurityToken(
           issuer: _jwtOptions.Issuer,
           audience: _jwtOptions.Audience,
           expires: DateTime.Now.AddHours(_jwtOptions.TokenValidityInHours),
           claims: claim,
           signingCredentials: credentials);

        string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}