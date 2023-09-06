using AutoMapper;
using PhoneBookAPI.Data.Entities;
using PhoneBookAPI.Models;
using PhoneBookAPI.Repositories.Interfaces;
using PhoneBookAPI.Services.Interfaces;

namespace PhoneBookAPI.Services.Services
{
    public class UserContactsService : IUserContactsService
    {
        #region Constructor and D.I
        private readonly IUserContactsRespository _userContactsRespository;
        private readonly ILogger<UserContactsService> _logger;
        private readonly IMapper _mapper;
        /// <summary>
        /// Add D.I
        /// </summary>
        /// <param name="userContactsRespository"></param>
        /// <param name="mapper"></param>
        public UserContactsService(IUserContactsRespository userContactsRespository, IMapper mapper, ILogger<UserContactsService> logger)
        {
            _userContactsRespository = userContactsRespository;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion
        #region mapper
        private static readonly Mapper _localmapper;
        private static readonly MapperConfiguration _localmappercfg;
        static UserContactsService()
        {
            _localmappercfg = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserContacts, UserContactsViewModel>();
                cfg.CreateMap<UserContactSaveModel, UserContacts>();
                cfg.CreateMap<UserContactUpdateModel, UserContacts>();
            });
            _localmapper = new Mapper(_localmappercfg);
        }
        #endregion
        ///<inheritdoc />
        public async Task<int> AddSync(UserContactSaveModel inputModel)
        {
            try
            {
                UserContacts userContact = new UserContacts();
                userContact = _localmapper.Map<UserContactSaveModel, UserContacts>(inputModel);
                userContact.CreatedAt = DateTime.Now;
                userContact.IsActive = true;
                return await _userContactsRespository.AddAsync(userContact);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"UserContactsService - adding new usercontact failed");
                throw;
            }
        }

        ///<inheritdoc/>
        public async Task<int> UpdateSync(UserContactUpdateModel updateModel, long UserContactId)
        {
            try
            {
                var usercontacts = await _userContactsRespository.GetbyIdAsync(UserContactId);
                if (!string.IsNullOrWhiteSpace(updateModel.LandLineNo))
                    usercontacts.LandLineNo = updateModel.LandLineNo;
                usercontacts.LastName = updateModel.LastName;
                usercontacts.AlternateMobileNo = updateModel.AlternateMobileNo;
                if (updateModel.IsActive.HasValue)
                    usercontacts.IsActive = (bool)updateModel.IsActive;
                usercontacts.UpdatedAt = DateTime.Now;
                return await _userContactsRespository.UpdateAsync(usercontacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"UserContactsService - UpdateSync usercontact failed");
                throw;
            }
        }
        ///<inheritdoc/>
        public async Task<List<UserContactsViewModel>> GetbyUserAsync(long UserId)
        {
            try
            {
                var usercontacts = await _userContactsRespository.GetbyUserAsync(UserId);
                return _localmapper.Map<List<UserContacts>, List<UserContactsViewModel>>(usercontacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"UserContactsService - GetbyUser failed");
                throw;
            }
        }
        ///<inheritdoc/>
        public async Task<UserContactsViewModel> GetbyIdAsync(long UserContactId)
        {
            try
            {
                var usercontacts = await _userContactsRespository.GetbyIdAsync(UserContactId);
                return _localmapper.Map<UserContacts, UserContactsViewModel>(usercontacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"UserContactsService - GetbyId failed");
                throw;
            }
        }
        ///<inheritdoc/>
        public async Task<int> RemoveAsync(long UserContactId)
        {
            try
            {
                return await _userContactsRespository.RemoveAsync(UserContactId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"UserContactsService - RemoveAsync failed");
                throw;
            }
        }
    }
}
