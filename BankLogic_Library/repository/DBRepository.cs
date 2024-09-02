using BankLogic_Library.DB;
using Lesson10;
using Lesson10.repository;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BankLogic_Library.repository
{
	public class DBRepository : Repository
	{
		private readonly MSSQLMyUserDbEntities db;

		public DBRepository(int countOfDepartments, int countOfClients) : base(countOfDepartments, countOfClients)
		{
			db = new MSSQLMyUserDbEntities();
			if (db.Clients.Count() == 0)
			{
				CreateClients(countOfClients);
			}
		}
		public override void CreateClients(int countOfClients)
		{
			base.CreateClients(countOfClients);
			SaveClientContext();
		}

		public override void SaveClientContext()
		{
			foreach (var client in Clients)
			{
				try
				{
					db.Clients.Add(client);

				}
				catch (Exception e)
				{
					Console.WriteLine($"{e.Message}");
				}
			}
			db.SaveChanges();
		}

		public override async Task GetClientContextAsync()
		{
			Clients = new ObservableCollection<Clients>(db.Clients);
			if (Clients == null)
			{
				Post.PostMessage("Нет клиентов");
			}
		}
	}
}
