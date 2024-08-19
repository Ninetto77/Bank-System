using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;


namespace Lesson10
{
	/// <summary>
	/// Логика взаимодействия для LoadWindow.xaml
	/// </summary>
	public partial class LoadWindow : Window
	{
		Worker worker;
		public LoadWindow()
		{
			FindOutWorker();
			InitializeComponent();
			InitButtons();
		}
		#region Инициализация
		/// <summary>
		/// начальное окно, спрашивающее, является ли пользователь менеджером или консультантом
		/// </summary>
		/// <returns></returns>
		private Worker FindOutWorker()
		{
			MessageBoxResult result = MessageBox.Show(
			"Вы менеджер?",
			 "Консультант или менеджер",
			 MessageBoxButton.YesNo,
			 MessageBoxImage.Information
		);

			if (result == MessageBoxResult.Yes)
				worker = new Manager();
			else
				worker = new Consultate();

			return worker;
		}

		/// <summary>
		/// Инициализация кнопок
		/// </summary>
		private void InitButtons()
		{
			LoadClientsXMLBtn.Click += (s, e) => Task.Run(async () => await LoadClientsAsync("XML")); ;
			LoadClientsDBBtn.Click += (s, e) => Task.Run(async () => await LoadClientsAsync("DB")); ;
		}
		#endregion

		/// <summary>
		/// Загрузка клиентов асинхронно
		/// </summary>
		/// <returns></returns>
		private async Task LoadClientsAsync(string place)
		{
			this.Dispatcher.Invoke(() =>
			{
				LoadClientsDBBtn.IsEnabled = false;
				LoadClientsXMLBtn.IsEnabled = false;
				ClientProgressBar.Value = 0;
				TextProgressBar.Text = "Начинаем загрузку...";
			});

			await UploadClientsAsync(place);

			this.Dispatcher.Invoke(() =>
			{
				LoadClientsDBBtn.IsEnabled = false;
				LoadClientsXMLBtn.IsEnabled = false;
				ContinueBtn.Visibility = Visibility;
				ClientProgressBar.Value = 100;
				TextProgressBar.Text = "Готово!";
			});
		}

		/// <summary>
		/// Начать загрузку клиентов 
		/// </summary>
		/// <param name="place"></param>
		/// <returns></returns>
		private async Task UploadClientsAsync(string place)
		{
			switch(place)
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

		/// <summary>
		/// Кнопка продолжения
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ContinueBtn_Click(object sender, RoutedEventArgs e)
		{
			MainWindow main = new MainWindow(worker);
			main.Show();

			this.Close();
		}


	}
}
