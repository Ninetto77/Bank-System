using Lesson10;
using Lesson10.repository;
using System.Threading.Tasks;

namespace BankLogic_Library.repository
{
	public class DBRepository : Repository
	{
		public DBRepository(int countOfDepartments, int countOfClients) : base(countOfDepartments, countOfClients)
		{
		}
		public override void CreateClients(int countOfClients)
		{
			base.CreateClients(countOfClients);
			SaveClientContext();
		}

		public override void SaveClientContext()
		{
		}

		public override async Task GetClientContextAsync()
		{
			//SqlConnectionStringBuilder strCon = new SqlConnectionStringBuilder()
			//{
			//	DataSource = @"(localdb)\MSS123QLLocalDB",
			//	InitialCatalog = "MSSQLMyUsersDb",
			//	IntegratedSecurity = true,
			//	// UserID = "Admin", Password = "qwerty",
			//	Pooling = false
			//};

			//SqlConnection connection = new SqlConnection()
			//{
			//	ConnectionString = strCon.ConnectionString
			//};

			//connection.StateChange += (s, e) =>
			//{
			//	Console.WriteLine($@"{nameof(connection)} в состоянии:" +
			//		$" {(s as SqlConnection).State}");
			//};

			//try
			//{
			//	await connection.OpenAsync();
			//}
			//catch (Exception e)
			//{
			//	Post.PostErrorMessage(e.Message);
			//}
			//finally
			//{
			//	connection.Close();
			//}
		}
	}
}
