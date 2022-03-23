using MgAPI.Data.Entities;

namespace MgAPI.Business.Authorization.Interfaces
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
        public string ValidateJwtToken(string token);
    }
}
