using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PhoneBookAPI.Common;
using PhoneBookAPI.Infrastructure.Interfaces;
using PhoneBookAPI.Models;
using PhoneBookAPI.Models.Common;
using PhoneBookAPI.Models.Users;
using PhoneBookAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhoneBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;

        }
        // GET: api/<UserContactsController>
        //[HttpGet]
        //public async Task<Response> Get([FromHeader] long UserId)
        //{
        //    var usercontacts = await _userContactsService.GetbyUser(UserId);
        //    return new Response()
        //    {
        //        Data = usercontacts,
        //        Message = "Success",
        //        Status = StatusCodes.Status200OK
        //    };
        //}

        // GET api/<UserContactsController>/5
        //[HttpGet("{id}")]
        //public async Task<Response> Get(int id)
        //{
        //    var result = await _userContactsService.GetbyId(id);
        //    return new Response()
        //    {
        //        Data = result,
        //        Message = "Data Saved",
        //        Status = StatusCodes.Status200OK
        //    };
        //}

        // POST api/<UserContactsController>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<Response> Post([FromBody] UserLoginModel value)
        {


            try
            {


                var result = await _userService.GetUserByUserNameOrEmailAsync(value.UserName, value.UserName);
            
                if (result == null)
                {
                    return new Response()
                    {
                        Data = "Invalid UserName or Email",
                        Message = "Please Provide Valid UserName or EmailAddress",
                        Status = StatusCodes.Status400BadRequest,

                    };
                }
                bool ismatchPassword = await _userService.UserLoginAsync(value);
                if (ismatchPassword)
                {
                    // create token
                    return new Response()
                    {
                        Message = "Success",
                        Data = CommonMethods.CreateToken(result),
                        Status = StatusCodes.Status200OK,

                    };
                }

                return new Response()
                {
                    Data = "Please Provide Valid Password",
                    Message = "Login Failed",
                    Status = StatusCodes.Status400BadRequest,

                };
            }
            catch (Exception ex)
            {

                return new Response()
                {
                    Message = "Something went wrong, please contact administrator",
                    Data = $"login Failed due to :{ex.Message}",
                    Status = StatusCodes.Status500InternalServerError,

                };
            }


        }

        [HttpPost("Signup")]
        [AllowAnonymous]
        public async Task<Response> Signup([FromBody] UserInputModel value)
        {
            try
            {

                var result = await _userService.GetUserByUserNameOrEmailAsync(value.UserName, value.EmailAddress);

                if (result != null)
                {
                    return new Response()
                    {
                        Data = "duplicate UserName or EmailAddress",
                        Message = "Please Provide Valid UserName or EmailAddress",
                        Status = StatusCodes.Status400BadRequest,

                    };
                }
                int issaved = await _userService.SaveUserAsync(value);
                if (issaved == 1)
                {
                    // create token
                    return new Response()
                    {
                        Message = "Success",
                        Data = "Signup Success",
                        Status = StatusCodes.Status200OK,

                    };
                }
                return new Response()
                {
                    Message = "Signup Failed",
                    Data = "Please Provide Valid UserName or EmailAddress",
                    Status = StatusCodes.Status400BadRequest,

                };


            }
            catch (Exception ex)
            {

                return new Response()
                {
                    Message = "Something went wrong, please contact administrator",
                    Data = $"Signup Failed due to :{ex.Message}",
                    Status = StatusCodes.Status500InternalServerError,

                };
            }

        }
        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<Response> Post([FromHeader] string NewPassword, [FromHeader] string Authorization)
        {

            return new Response();
            //try
            //{


               

            //    if (result == null)
            //    {
            //        return new Response()
            //        {
            //            Data = "Invalid UserName or Email",
            //            Message = "Please Provide Valid UserName or EmailAddress",
            //            Status = StatusCodes.Status400BadRequest,

            //        };
            //    }
            //    bool ismatchPassword = await _userService.UserLogin(value);
            //    if (ismatchPassword)
            //    {
            //        // create token
            //        return new Response()
            //        {
            //            Message = "Success",
            //            Data = CreateToken(result),
            //            Status = StatusCodes.Status200OK,

            //        };
            //    }

            //    return new Response()
            //    {
            //        Data = "Please Provide Valid Password",
            //        Message = "Login Failed",
            //        Status = StatusCodes.Status400BadRequest,

            //    };
            //}
            //catch (Exception ex)
            //{

            //    return new Response()
            //    {
            //        Message = "Something went wrong, please contact administrator",
            //        Data = $"login Failed due to :{ex.Message}",
            //        Status = StatusCodes.Status500InternalServerError,

            //    };
            //}


        }
        // PUT api/<UserContactsController>/5
        //[HttpPut("{id}")]
        //public async Task<Response> Put(long id, [FromBody] UserContactUpdateModel updateModel)
        //{
        //    var result= await _userContactsService.UpdateSync(updateModel,id);
        //    return new Response()
        //    {
        //        Data = result,
        //        Message = "Data updated",
        //        Status = StatusCodes.Status200OK
        //    };
        //}

        //// DELETE api/<UserContactsController>/5
        //[HttpDelete("{id}")]
        //public async Task<Response> Delete(int id)
        //{
        //    var result= await _userContactsService.RemoveAsync(id);
        //    return new Response()
        //    {
        //        Data = result,
        //        Message = "Data Deleted",
        //        Status = StatusCodes.Status200OK
        //    };
        //}

        
        
    }
}
