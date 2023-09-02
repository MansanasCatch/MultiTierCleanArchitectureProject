using Microsoft.AspNetCore.Identity;

namespace PracticeInventory.Domain.Interfaces;

public interface IJwtProvider
{
    Task<string> Generate(IdentityUser user);
}