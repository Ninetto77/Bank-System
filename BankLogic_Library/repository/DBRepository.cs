using BankLogic_Library.DB;
using Lesson10;
using Lesson10.repository;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BankLogic_Library.repository
{
	public class DBRepository : Repository
	{
		private ClientContex db;

		public DBRepository(int countOfDepartments, int countOfClients) : base(countOfDepartments, countOfClients)
		{
			db = new ClientContex();
		}
		public override void CreateClients(int countOfClients)
		{
			base.CreateClients(countOfClients);
			SaveClientContext();
		}

		public override void SaveClientContext()
		{
			db.SaveChanges();
		}

		public override async Task GetClientContextAsync()
		{
			ClientContex db = new ClientContex();
			var clients = db.Clients;

			var clients1 = new ObservableCollection<Client>(clients);
			if (Clients == null)
			{
				Post.PostMessage("Нет клиентов");
			}
		}
	}
}
