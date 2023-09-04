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
        public UserContactsController(IUserContactsService userContactsService, IUserService userService)
        {
            _userContactsService = userContactsService;
            _userService = userService;
        }
        // GET: api/<UserContactsController>
        [HttpGet]
        public async Task<Response> Get()
        {
            string token = HttpContext.Request.GetToken();
            string userName = HttpRequestExtension.GetUserName(token);
            long UserId = await _userService.getUserId(userName);
            var usercontacts = await _userContactsService.GetbyUser(UserId);
            return new Response()
            {
                Data = usercontacts,
                Message = "Success",
                Status = StatusCodes.Status200OK
            };
        }

        // GET api/<UserContactsController>/5
        [HttpGet("{id}")]
        public async Task<Response> Get(int id)
        {
            var result = await _userContactsService.GetbyId(id);
            return new Response()
            {
                Data = result,
                Message = "Data Saved",
                Status = StatusCodes.Status200OK
            };
        }

        // POST api/<UserContactsController>
        [HttpPost]
        public async Task<Response> Post([FromBody] UserContactInputModel value)
        {

            // var token=  context.HttpContext.Request.GetToken();

            var result = await _userContactsService.AddSync(value);
            return new Response()
            {
                Data = result,
                Message = "Data Saved",
                Status = StatusCodes.Status200OK
            };
        }

        // PUT api/<UserContactsController>/5
        [HttpPut("{id}")]
        public async Task<Response> Put(long id, [FromBody] UserContactUpdateModel updateModel)
        {
            var result = await _userContactsService.UpdateSync(updateModel, id);
            return new Response()
            {
                Data = result,
                Message = "Data updated",
                Status = StatusCodes.Status200OK
            };
        }

        // DELETE api/<UserContactsController>/5
        [HttpDelete("{id}")]
        public async Task<Response> Delete(int id)
        {
            var result = await _userContactsService.RemoveAsync(id);
            return new Response()
            {
                Data = result,
                Message = "Data Deleted",
                Status = StatusCodes.Status200OK
            };
        }
    }
}
