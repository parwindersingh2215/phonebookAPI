using Microsoft.EntityFrameworkCore;
using PhoneBookAPI.Data;
using PhoneBookAPI.Data.Entities;
using PhoneBookAPI.Repositories.Interfaces;

namespace PhoneBookAPI.Repositories.Respositories
{
    public class UserContactsRespository : IUserContactsRespository
    {
        private readonly PhoneBookDBContext _dbContext;

        

        public UserContactsRespository(PhoneBookDBContext DBContext)
        {
            _dbContext = DBContext;

        }
        /// <summary>
        /// create new user contact
        /// </summary>
        /// <param name="userContacts"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(UserContacts userContacts)
        {
            await _dbContext.UserContacts.AddAsync(userContacts);
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// get contactdetails by Id
        /// </summary>
        /// <param name="UserContactId"></param>
        /// <returns></returns>
        public async Task<UserContacts> GetbyIdAsync(long UserContactId)
        {
            return await _dbContext.UserContacts.Where(x => x.UserContactId == UserContactId && x.IsDeleted != true).FirstOrDefaultAsync();
        }

        /// <summary>
        /// get usercontacts by userid
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<List<UserContacts>> GetbyUserAsync(long UserId)
        {
            return await _dbContext.UserContacts.Where(x => x.UserId == UserId && x.IsActive==true && x.IsDeleted != true).ToListAsync();
        }
        /// <summary>
        /// delete usercontact by id (soft del)
        /// </summary>
        /// <param name="UserContactId"></param>
        /// <returns></returns>
        public async Task<int> RemoveAsync(long UserContactId)
        {
            UserContacts userContacts=  await _dbContext.UserContacts.Where(x=>x.UserContactId==UserContactId).FirstOrDefaultAsync();
           
            if (userContacts == null)
            {
                return 0;
            }
            userContacts.IsDeleted = true;
            userContacts.UpdatedAt = DateTime.Now;
            userContacts.DeletedAt = DateTime.Now;
           _dbContext.Attach(userContacts);
            _dbContext.Entry(userContacts).State= EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// update user contact
        /// </summary>
        /// <param name="userContacts"></param>
        /// <returns></returns>

        public async Task<int> UpdateAsync(UserContacts userContacts)
        {
            _dbContext.Attach(userContacts);
            _dbContext.Entry(userContacts).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }
    }
}
