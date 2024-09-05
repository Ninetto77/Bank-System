using BankLogic_Library.MVP.Presenters;
using BankLogic_Library.MVP.Views;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace Lesson10
{
	/// <summary>
	/// Логика взаимодействия для LoadWindow.xaml
	/// </summary>
	public partial class LoadWindow : Window, IViewLoad
	{
		Worker worker;
		LoadPresenter p;

		public bool IsEnableButton
		{
			set
			{
				LoadClientsXMLBtn.IsEnabled = value;
				LoadClientsDBBtn.IsEnabled = value;
			}
		}
		public float ProgressBarValue { set => ClientProgressBar.Value = value; }
		public string ProgressBarText { set => TextProgressBar.Text = value; }

		public LoadWindow(Worker worker)
		{
			this.worker = worker;

			p = new LoadPresenter(this, worker);
			p.OnFinishLoading += ShowContinueButton;
			InitializeComponent();
			InitButtons();
		}

		/// <summary>
		/// При окончании загрузки скрыть /открыть нужные кнопки
		/// </summary>
		private void ShowContinueButton()
		{
			ContinueBtn.Visibility = Visibility.Visible;
			LoadClientsDBBtn.Visibility = Visibility.Hidden;
			LoadClientsXMLBtn.Visibility = Visibility.Hidden;
		}

		#region Инициализация
		/// <summary>
		/// Инициализация кнопок
		/// </summary>
		private void InitButtons()
		{
			LoadClientsXMLBtn.Click += (s, e) => Task.Run(async () => await StartLoad("XML"));
			LoadClientsDBBtn.Click += (s, e) => Task.Run(async () => await StartLoad("DB"));
			CloseBtn.Click += (s, e) => Application.Current.Shutdown();
		}
		#endregion

		private async Task StartLoad(string place)
		{
			this.Dispatcher.Invoke( () =>
			{
				p.ChangeButtons(true);
			});

			try
			{
				var task = p.StartLoad(place);
				await task;

			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			this.Dispatcher.Invoke( () =>
			{
				p.ChangeButtons(false);
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

		private void Border_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				this.DragMove();
			}
		}
	}
}
