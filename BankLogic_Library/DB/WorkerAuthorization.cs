using BankLogic_Library.Authorization;
using BankLogic_Library.workers;
using Lesson10;
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
		}

		public WorkerAuthorization(string login, string password, WorkerType worker)
		{
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
			Task<bool> task =  IsUserExists();
			await task;
			if (task.Result == true) return;
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
		/// Существует ли пользователь в бд
		/// </summary>
		/// <returns>Пользователя нет в бд</returns>
		private async Task<bool> IsUserExists()
		{
			try
			{
				await connection.OpenAsync();

				DataTable table = new DataTable();
				SqlDataAdapter adapter = new SqlDataAdapter();


				var sql = "SELECT * FROM [Users] WHERE [Login]=@login";

				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add("@login", SqlDbType.VarChar).Value = login;
					command.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
					command.Parameters.Add("@workerType", SqlDbType.VarChar).Value = workerType.ToString();

					adapter.SelectCommand = command;
					adapter.Fill(table);

					if (table.Rows.Count > 0)
					{
						MessageBox.Show("Такой пользователь уже есть! Введите другой логин");
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"{e.Message}");
			}
			finally
			{
				connection.Close();
			}
			return false;
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


				var sql = "SELECT * FROM [Users] WHERE [Login]=@login  AND [Password]=@password AND [WorkerType] = @workerType";

				using ( SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add("@login", SqlDbType.VarChar).Value = login;
					command.Parameters.Add("@password", SqlDbType.VarChar).Value =  password;
					command.Parameters.Add("@workerType", SqlDbType.VarChar).Value = workerType.ToString();

					adapter.SelectCommand = command;
					adapter.Fill(table);

					if (table.Rows.Count > 0)
					{
						MessageBox.Show("Вы вошли в систему!");
					}
					else
						MessageBox.Show("Такого пользователя нет");
				}
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

				var sql = "INSERT INTO Users( [Login], [Password], [WorkerType])" +
					" VALUES( @Login, @Password, @WorkerType)";

				using (var command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add("@Login", SqlDbType.VarChar).Value = login;
					command.Parameters.Add("@Password", SqlDbType.VarChar).Value  =password;
					command.Parameters.Add("@WorkerType", SqlDbType.VarChar).Value  =workerType.ToString();

					if (command.ExecuteNonQuery() == 1) Post.PostMessage("Вы успешно зарегистрировались!\nВойдите в свой аккаунт");
					else Post.PostErrorMessage("Аккаунт не был создан");
				}
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
