using BankLogic_Library.MVP.Views;
using Lesson10;
using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BankLogic_Library.MVP.Presenters
{
	public class LoadPresenter
	{
		public Action OnFinishLoading;
		private IViewLoad view;
		private Worker worker;
		public LoadPresenter(IViewLoad view, Worker worker)
		{
			this.view = view;
			this.worker = worker;
		}

		public async Task StartLoad(string place)
		{
			var task = UploadClientsAsync(place);
			await task;
		}

		/// <summary>
		/// Поменять кнопки
		/// </summary>
		/// <param name="isFinish">это финальная часть программы?</param>
		public void ChangeButtons(bool isFinish)
		{
			if (isFinish)
			{
				try
				{
					view.IsEnableButton = false;
					view.ProgressBarValue = 0;
					view.ProgressBarText = "Начинаем загрузку...";
				}
				catch
				{
					Post.PostErrorMessage("smt wrong");
				}
			}
			else
			{
				FinishLoad();
			}
		}

		/// <summary>
		/// Поменять финальные кнопки
		/// </summary>
		public void FinishLoad()
		{
			view.IsEnableButton = false;
			view.ProgressBarValue = 100;
			view.ProgressBarText = "Готово";

			OnFinishLoading?.Invoke();
		}

		/// <summary>
		/// Начать загрузку клиентов 
		/// </summary>
		/// <param name="place"></param>
		/// <returns></returns>
		private async Task UploadClientsAsync(string place)
		{
			switch (place)
			{
				case "XML":
					var task = worker.UploadClientsFromXMLAsync();
					await task;
					break;
				case "DB":
					var task1 = worker.UploadClientsFromDBAsync();
					await task1;
					break;
			}
		}
	}
}
