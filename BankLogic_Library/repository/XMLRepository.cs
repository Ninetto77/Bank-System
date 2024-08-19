using Lesson10.utills;
using Lesson10;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Lesson10.repository;

namespace BankLogic_Library.repository
{
	public class XMLRepository: Repository
	{
		private const string filePath = "clientdb.xml";

		public XMLRepository(int countOfDepartments, int countOfClients) : base(countOfDepartments, countOfClients)
		{
		}

		public override void CreateClients(int countOfClients)
		{
			base.CreateClients(countOfClients);
			SaveClientContext();
		}

		public override void SaveClientContext()
		{
			XMALWork.SerializeFields(Clients, filePath);
		}

		public override async Task GetClientContextAsync()
		{
			var task = XMALWork.DeserializeField<Client>(filePath);
			await task;

			Clients = task.Result as ObservableCollection<Client>;

			if (Clients == null)
			{
				Post.PostMessage("Нет клиентов");
			}
		}
	}
}
