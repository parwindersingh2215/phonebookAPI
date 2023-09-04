using Microsoft.EntityFrameworkCore;
using PhoneBookAPI.Data.Entities;

namespace PhoneBookAPI.Data
{
    public class PhoneBookDBContext:DbContext
    {
     
        public PhoneBookDBContext(DbContextOptions<PhoneBookDBContext> options)
            : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<UserContacts> UserContacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
        }
    }
}
