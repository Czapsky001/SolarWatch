using Microsoft.AspNetCore.Identity;

namespace SolarWatch.Services.TokenService
{
    public interface ITokenService
    {
        public string CreateToken(IdentityUser user);
    }
}
