using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PhoneBookAPI.Common;
using PhoneBookAPI.Filters;
using PhoneBookAPI.Models;
using PhoneBookAPI.Models.Common;
using PhoneBookAPI.Services.Interfaces;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhoneBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [PhoneBookAuthorization("")]
    public class UserContactsController : ControllerBase
    {
        private readonly IUserContactsService _userContactsService;
        private readonly IUserService _userService;
        private readonly ILogger<UserContactsController> _logger;
        public UserContactsController(IUserContactsService userContactsService, IUserService userService, ILogger<UserContactsController> logger)
        {
            _userContactsService = userContactsService;
            _userService = userService;
            _logger = logger;

        }
        // GET: api/<UserContactsController>
        [HttpGet]
        public async Task<Response> Get()
        {
            try
            {
                string token = HttpContext.Request.GetToken();
                string userName = HttpRequestExtension.GetUserName(token);
                long UserId = await _userService.getUserIdAsync(userName);
                var usercontacts = await _userContactsService.GetbyUserAsync(UserId);
                return new Response()
                {
                    Data = usercontacts,
                    Message = "Success",
                    Status = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UserContactsController - get usercontacts failed");
                throw;
            }
        }

        // GET api/<UserContactsController>/5
        [HttpGet("{id}")]
        public async Task<Response> Get(int id)
        {
            try
            {
                var result = await _userContactsService.GetbyIdAsync(id);
                return new Response()
                {
                    Data = result,
                    Message = "Data Saved",
                    Status = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UserContactsController - get by id usercontacts failed");
                throw;
            }
        }

        // POST api/<UserContactsController>
        [HttpPost]
        public async Task<Response> Post([FromBody] UserContactInputModel value)
        {
            try
            {
                string token = HttpContext.Request.GetToken();
                string userName = HttpRequestExtension.GetUserName(token);
                long UserId = await _userService.getUserIdAsync(userName);
                UserContactSaveModel userContactSaveModel = new UserContactSaveModel()
                {
                    AlternateMobileNo = value.AlternateMobileNo,
                    FirstName = value.FirstName,
                    LandLineNo = value.LandLineNo,
                    LastName = value.LastName,
                    MobileNo = value.MobileNo,
                    UserId = UserId
                };
                var result = await _userContactsService.AddSync(userContactSaveModel);
                return new Response()
                {
                    Data = result,
                    Message = "Success",
                    Status = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UserContactsController - Post usercontacts failed");
                throw;
            }
        }

        // PUT api/<UserContactsController>/5
        [HttpPut("{id}")]
        public async Task<Response> Put(long id, [FromBody] UserContactUpdateModel updateModel)
        {
            try
            {
                var result = await _userContactsService.UpdateSync(updateModel, id);
                return new Response()
                {
                    Data = result,
                    Message = "Data updated",
                    Status = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UserContactsController - Post usercontacts failed");
                throw;
            }
        }

        // DELETE api/<UserContactsController>/5
        [HttpDelete("{id}")]
        public async Task<Response> Delete(int id)
        {
            try
            {
                var result = await _userContactsService.RemoveAsync(id);
                return new Response()
                {
                    Data = result,
                    Message = "Data Deleted",
                    Status = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UserContactsController - Post usercontacts failed");
                throw;
            }
        }
    }
}
