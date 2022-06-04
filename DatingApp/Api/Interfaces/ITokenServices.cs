using Api.Entities;

namespace Api.Interfaces
{
    public interface ITokenServices
    {
        string CreateToken(AppUser user);
    }
}
