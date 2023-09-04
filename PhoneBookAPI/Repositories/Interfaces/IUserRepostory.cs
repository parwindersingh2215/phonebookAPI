using PhoneBookAPI.Data.Entities;

namespace PhoneBookAPI.Repositories.Interfaces
{
    public interface IUserRepostory
    {

        Task<Users> GetUserByUserNameOrEmailAsync(string UserName, string Email);
        Task<int> SaveUserAsync(Users  users);
        Task<long> getUserId(string UserName);
    }
}
