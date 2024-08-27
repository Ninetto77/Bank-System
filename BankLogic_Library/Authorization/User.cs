using BankLogic_Library.workers;

namespace BankLogic_Library.DB
{
	public class User
	{
		private static int idUser;

		static User()
		{
			idUser = 2;
		}

        public User()
        {
			this.id = (idUser++).ToString();
        }

        private string id { get; set; }
		private string login{get; set;}
		private string password{get; set;}
		private WorkerType workerType{get; set;}
	}
}
