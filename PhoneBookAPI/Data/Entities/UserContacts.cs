using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBookAPI.Data.Entities
{
    public class UserContacts
    {
        [Key]
        public long UserContactId { get; set; }

        
        public Users Users { get; set; }

        [ForeignKey("Users")]
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string LandLineNo { get; set; }
        public string AlternateMobileNo { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }=false;
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
