using PhoneBookAPI.Data.Entities;
using PhoneBookAPI.Models.Users;

namespace PhoneBookAPI.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Get User by email or username
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        Task<UserViewModel> GetUserByUserNameOrEmail(string UserName, string Password);
        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="userLoginModel"></param>
        /// <returns></returns>
        Task<bool> UserLogin(UserLoginModel userLoginModel);
        /// <summary>
        /// Save new User details(Signup process)
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        Task<int> SaveUserAsync(UserInputModel users);
        /// <summary>
        /// get user details by UserId
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        Task<long> getUserId(string UserName);
    }
}
