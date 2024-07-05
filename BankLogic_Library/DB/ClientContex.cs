using Lesson10;
using System.Data.Entity;

namespace BankLogic_Library.DB
{
    public class ClientContex: DbContext
    {
        public ClientContex() : base("DbConnection") { }

        public DbSet<Client> Clients { get; set; }
    }
}
