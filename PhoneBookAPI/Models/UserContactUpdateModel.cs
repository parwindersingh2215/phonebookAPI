namespace PhoneBookAPI.Models
{
    public class UserContactUpdateModel
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MobileNo { get; set; }
        public string? LandLineNo { get; set; }
        public string? AlternateMobileNo { get; set; }
        public bool? IsActive { get; set; }
       
    }
}
