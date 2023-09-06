using AutoMapper;
using PhoneBookAPI.Data.Entities;
using PhoneBookAPI.Infrastructure.Interfaces;
using PhoneBookAPI.Models;
using PhoneBookAPI.Models.Users;
using PhoneBookAPI.Repositories.Interfaces;
using PhoneBookAPI.Services.Interfaces;

namespace PhoneBookAPI.Services.Services
{
    public class UserService : IUserService
    {

        #region Constructor and D.I
        private readonly IUserRepostory _userRepostory;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        /// <summary>
        /// Add D.I
        /// </summary>
        /// <param name="userRepostory"></param>
        /// <param name="mapper"></param>
        public UserService(IUserRepostory userRepostory, IMapper mapper, IPasswordHasher passwordHasher, ILogger<UserService> logger)
        {
            _userRepostory = userRepostory;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }
        #endregion
        #region mapper
        private static readonly Mapper _localmapper;
        private static readonly MapperConfiguration _localmappercfg;
        static UserService()
        {
            _localmappercfg = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Users, UserViewModel>();
                cfg.CreateMap<UserInputModel, Users>();

            });
            _localmapper = new Mapper(_localmappercfg);
        }
        #endregion

        /// <summary>
        /// serch userdetails either by username or password
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        public async Task<UserViewModel> GetUserByUserNameOrEmailAsync(string UserName, string Password)
        {
            try
            {


                var UserData = await _userRepostory.GetUserByUserNameOrEmailAsync(UserName, Password);
                return _localmapper.Map<UserViewModel>(UserData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUserByUserNameOrEmail failed");
                throw;
            }
        }

        /// <summary>
        /// verify password for user login
        /// </summary>
        /// <param name="userLoginModel"></param>
        /// <returns></returns>
        public async Task<bool> UserLoginAsync(UserLoginModel userLoginModel)
        {
            try
            {
                var userdata = await _userRepostory.GetUserByUserNameOrEmailAsync(userLoginModel.UserName, userLoginModel.UserName);
                if (userdata != null)
                {
                    return _passwordHasher.Verify(userdata.PasswordHash, userLoginModel.Password);
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UserLogin failed");
                throw;
            }

        }

        /// <summary>
        /// Save UserDetails
        /// </summary>
        /// <param name="userInputModel"></param>
        /// <returns></returns>
        public async Task<int> SaveUserAsync(UserInputModel userInputModel)
        {
            try
            {

         
            var userentity = _localmapper.Map<Users>(userInputModel);
            (var hash, var salt) = _passwordHasher.Hash(userInputModel.Password);
            userentity.IsActive = true;
            userentity.CreatedAt = DateTime.Now;
            userentity.PasswordHash = hash;
            userentity.PasswordSalt = salt;
                return await _userRepostory.SaveUserAsync(userentity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SaveUserAsync failed");
                throw;
            }
           
        }

        /// <summary>
        /// get userid by username
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public async Task<long> getUserIdAsync(string UserName)
        {
            return await _userRepostory.getUserId(UserName);
        }
    }
}
