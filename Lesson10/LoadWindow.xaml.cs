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
		#endregion

		/// <summary>
		/// Кнопка загрузки клиентов
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoadClientsBtn_Click(object sender, RoutedEventArgs e)
		{
			Task.Run(async () => await LoadClientsAsync());
		}
		/// <summary>
		/// Загрузка клиентов асинхронно
		/// </summary>
		/// <returns></returns>
		private async Task LoadClientsAsync()
		{
			this.Dispatcher.Invoke(() =>
			{
				LoadClientsBtn.IsEnabled = false;
				ClientProgressBar.Value = 0;
				TextProgressBar.Text = "Начинаем загрузку...";
			});

			var task = worker.UploadClientsFromFileAsync();
			await task;

			this.Dispatcher.Invoke(() =>
			{
				LoadClientsBtn.IsEnabled = false;
				ContinueBtn.Visibility = Visibility;
				ClientProgressBar.Value = 100;
				TextProgressBar.Text = "Готово!";
			});
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
