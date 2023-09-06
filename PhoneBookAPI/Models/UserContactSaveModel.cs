using System.ComponentModel.DataAnnotations;

namespace PhoneBookAPI.Models
{
    public class UserContactSaveModel
    {

        public long UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string LandLineNo { get; set; }
        public string AlternateMobileNo { get; set; }

    }
}
