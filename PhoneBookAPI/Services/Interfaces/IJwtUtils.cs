using PhoneBookAPI.Data.Entities;

namespace PhoneBookAPI.Services.Interfaces
{
    public interface IJwtUtils
    {
      
        public int? ValidateJwtToken(string? token);
    }
}
