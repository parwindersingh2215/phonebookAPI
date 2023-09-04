using PhoneBookAPI.Common;
using PhoneBookAPI.Models;

namespace PhoneBookAPI.Services.Interfaces
{
    public interface IUserContactsService
    {
       
        /// <summary>
        /// Create New User Contact
        /// </summary>
        /// <param name="userContacts"></param>
        /// <returns></returns>
        Task<int> AddSync(UserContactInputModel userContacts);
        /// <summary>
        /// Update UserContact details
        /// </summary>
        /// <param name="userContacts"></param>
        /// <param name="UserContactId"></param>
        /// <returns></returns>
        Task<int> UpdateSync(UserContactUpdateModel userContacts,long UserContactId);
        /// <summary>
        /// get all usercontacts by userid
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<List<UserContactsViewModel>> GetbyUserAsync(long UserId);
        /// <summary>
        /// get usercontact details by Id
        /// </summary>
        /// <param name="UserContactId"></param>
        /// <returns></returns>
        Task<UserContactsViewModel> GetbyIdAsync(long UserContactId);
        /// <summary>
        /// Delete UserConatct (Soft Delete by updating isdeleted column to true)
        /// </summary>
        /// <param name="UserContactId"></param>
        /// <returns></returns>
        Task<int> RemoveAsync(long UserContactId);

    }
}
