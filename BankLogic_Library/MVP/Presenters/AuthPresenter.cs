using BankLogic_Library.DB;
using BankLogic_Library.workers;
using BankLogic_Library.workers.workerFactory;
using Lesson10;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BankLogic_Library.MVP.Presenters
{
	public class AuthPresenter
	{
		public Action<IWorker> OnEnterInSystem;
		private IViewAuthWindow view;
		public AuthPresenter(IViewAuthWindow view) 
		{
			this.view = view;
		}

		#region Авторизация клиента
		/// <summary>
		/// Войти в систему
		/// </summary>
		public async void EnterToSystem()
		{
			if (!IsEmptyFields()) return;

			WorkerType type;
			type = view.isManager == true ? type = WorkerType.manager : type = WorkerType.consultant;

			WorkerAuthorization auth = new WorkerAuthorization(view.Login, view.Password, type);

			var task = auth.EnterToSystem();
			await task;

			if (auth.IsSuccess)
			{
				if (view.isManager == true)
					OnEnterInSystem?.Invoke(WorkerFactory.GetWorker(WorkerType.manager));
				else
					OnEnterInSystem?.Invoke(WorkerFactory.GetWorker(WorkerType.consultant));
			}
		}

		/// <summary>
		/// Регистрация клиента
		/// </summary>
		/// <returns></returns>
		public async Task RegistrateInSystem()
		{
			WorkerType type;
			type = view.isManager == true ? type = WorkerType.manager : type = WorkerType.consultant;

			Console.WriteLine("create WorkerAuthorization");
			WorkerAuthorization auth = new WorkerAuthorization(view.Login, view.Password, type);
			
			await auth.RegisterInSystem();
		}

		/// <summary>
		/// Проверка на пустые поля
		/// </summary>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public bool IsEmptyFields()
		{ 
			if (view.Login == "" && view.Password == "")
			{
				Post.PostErrorMessage("Введите поля!");
				return false;
			}
			return true;
		}

		#endregion
	}
}