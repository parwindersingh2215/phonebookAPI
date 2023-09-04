using Microsoft.EntityFrameworkCore;
using PhoneBookAPI.Data;
using PhoneBookAPI.Data.Entities;
using PhoneBookAPI.Repositories.Interfaces;

namespace PhoneBookAPI.Repositories.Respositories
{
    public class UserRepository : IUserRepostory
    {
        private readonly PhoneBookDBContext _dbContext;
        public UserRepository(PhoneBookDBContext DBContext)
        {
            _dbContext = DBContext;

        }
        /// <summary>
        /// search user by email or Username
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        public async Task<Users> GetUserByUserNameOrEmailAsync(string UserName, string Email)
        {
            return await _dbContext.Users.Where(x => x.UserName == UserName || x.EmailAddress == Email).FirstOrDefaultAsync();
        }

        /// <summary>
        /// get user by username
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public async Task<long> getUserId(string UserName)
        {
           return await _dbContext.Users.Where(x=>x.UserName == UserName).Select(x=>x.UserId).FirstOrDefaultAsync();
        }

        /// <summary>
        /// save new user
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public async Task<int> SaveUserAsync(Users users)
        {
            await _dbContext.Users.AddAsync(users);
         return   await _dbContext.SaveChangesAsync();
        }
    }
}
