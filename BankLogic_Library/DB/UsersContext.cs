using System.Data.Entity;

namespace BankLogic_Library.DB
{
	public class UsersContext : DbContext
	{
        public UsersContext() : base("DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }
	}
}
