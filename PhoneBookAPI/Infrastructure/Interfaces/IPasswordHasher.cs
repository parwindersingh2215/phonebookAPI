using PhoneBookAPI.Data.Entities;

namespace PhoneBookAPI.Infrastructure.Interfaces
{
    public interface IPasswordHasher
    {

        (string hash,string salt) Hash(string Password);
        bool Verify(string HashPassword,string Password);


    }
}
