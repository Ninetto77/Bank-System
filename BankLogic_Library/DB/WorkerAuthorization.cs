using BankLogic_Library.Authorization;
using BankLogic_Library.workers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;

namespace BankLogic_Library.DB
{
	public class WorkerAuthorization: IAthorization
	{

		private User user;

		private string id;
		private string login;
		private string password;
		private WorkerType workerType;



		private SqlConnection connection;
		UsersContext usersdb;

		#region Конструкторы

		static WorkerAuthorization()
		{
			//idUser = 2;
		}

		public WorkerAuthorization(string login, string password, WorkerType worker)
		{
			//this.id = (idUser++).ToString();
			this.login = login;
			this.password = password;
			this.workerType = worker;
		}

		public WorkerAuthorization(string id, string login, string password, WorkerType worker) 
		{
			this.id = id;
			this.login = login;
			this.password = password;
			this.workerType = worker;
		}

		#endregion

		public async void EnterToSystem()
		{
			OpenSQLConnection();
			await LoginWorker();
		}

		public async void RegisterInSystem()
		{
			OpenSQLConnection();
			await RegisterWorker();
		}

		/// <summary>
		/// Открыть соединения с бд
		/// </summary>
		private void OpenSQLConnection()
		{
			SqlConnectionStringBuilder strCon = new SqlConnectionStringBuilder()
			{
				DataSource = @"(localdb)\MSSQLLocalDB",
				InitialCatalog = "MSSQLMyUsersDb",
				IntegratedSecurity = true,
				// UserID = "Admin", Password = "qwerty",
				Pooling = false
			};

			connection = new SqlConnection()
			{
				ConnectionString = strCon.ConnectionString
			};

			connection.StateChange += (s, e) =>
			{
				Console.WriteLine($@"{nameof(connection)} в состоянии:" +
					$" {(s as SqlConnection).State}");
			};

		}

		/// <summary>
		/// Найти пользователя в бд
		/// </summary>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task LoginWorker()
		{
			try
			{
				await connection.OpenAsync();

				DataTable table = new DataTable();
				SqlDataAdapter adapter = new SqlDataAdapter();


				var sql = "SELECT * FROM [Users] WHERE 'Login'=@login  AND 'Password'=@password AND 'WorkerType' = @workerType";

				using ( SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add("@login", SqlDbType.VarChar).Value = login;
					command.Parameters.Add("@password", SqlDbType.VarChar).Value =  password;
					command.Parameters.Add("@workerType", SqlDbType.VarChar).Value = workerType.ToString();

					adapter.SelectCommand = command;
					adapter.Fill(table);

					if (table.Rows.Count > 0)
					{
						MessageBox.Show("yes");
					}
					else
						MessageBox.Show("no");

					//command.Parameters.Add(new SqlParameter("login", login));
					//command.Parameters.Add(new SqlParameter("password", password));
					//command.Parameters.Add(new SqlParameter("workerType", workerType.ToString()));

					//command.ExecuteNonQuery();
				}

				//SqlDataReader reader = ;


				//SqlCommand sqlCommand = connection.CreateCommand();
			}
			catch (Exception e)
			{
				Console.WriteLine($"{e.Message}");
			}
			finally
			{
				connection.Close();
			}
		}

		/// <summary>
		/// Добавить пользователя в бд
		/// </summary>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task RegisterWorker()
		{
			try
			{
				await connection.OpenAsync();

				var sql = "INSERT INTO Users([Id], [Login], [Password], [WorkerType])" +
					" VALUES(@id, @Login, @Password, @WorkerType)";

				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add(new SqlParameter("id", id));
					command.Parameters.Add(new SqlParameter("Login", login));
					command.Parameters.Add(new SqlParameter("Password", password));
					command.Parameters.Add(new SqlParameter("WorkerType", workerType.ToString()));

					command.ExecuteNonQuery();
				}
				

				//SqlCommand sqlCommand = connection.CreateCommand();
			}
			catch (Exception e)
			{
				Console.WriteLine($"{e.Message}");
			}
			finally
			{
				connection.Close();
			}
		}
	}
}
