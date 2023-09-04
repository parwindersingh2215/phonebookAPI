using System.ComponentModel.DataAnnotations;

namespace PhoneBookAPI.Models.Users
{
    public class UserLoginModel
    {

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
