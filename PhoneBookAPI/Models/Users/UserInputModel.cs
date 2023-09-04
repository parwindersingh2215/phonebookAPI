using System.ComponentModel.DataAnnotations;

namespace PhoneBookAPI.Models.Users
{
    public class UserInputModel
    {

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string EmailAddress { get; set; }

    }
}
