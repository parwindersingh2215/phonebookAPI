using PhoneBookAPI.Common;
using PhoneBookAPI.Data.Entities;

namespace PhoneBookAPI.Repositories.Interfaces
{
    public interface IUserContactsRespository
    {
        Task<int> AddAsync(UserContacts userContacts);
        Task<int> UpdateAsync(UserContacts userContacts);
        Task<List<UserContacts>> GetbyUserAsync( long UserId);
        Task<UserContacts> GetbyIdAsync(long UserContactId);
        Task<int> RemoveAsync(long UserContactId);

    }
}
