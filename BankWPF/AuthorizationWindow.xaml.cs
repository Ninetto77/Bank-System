using System.Threading.Tasks;
using System.Windows;
using BankLogic_Library.MVP;
using BankLogic_Library.MVP.Presenters;
using System.Windows.Input;
using System;

namespace Lesson10
{
	/// <summary>
	/// Логика взаимодействия для AuthorizationWindow.xaml
	/// </summary>
	public partial class AuthorizationWindow : Window, IViewAuthWindow
	{
		public string Login { get => LoginTextBox.Text; }
		public string Password { get => PasswordTextBox.Password; }
		public bool? isManager { get => WorkerCheckBox.IsChecked; }


		private readonly AuthPresenter p;

		/// <summary>
		/// Конструктор
		/// </summary>
		public AuthorizationWindow()
		{
			p = new AuthPresenter(this);
			p.OnEnterInSystem += OpenLoadWindow;
			InitializeComponent();
			InitButtons();
		}

		/// <summary>
		/// Инициализировать кнопки
		/// </summary>
		private void InitButtons()
		{
			EnterBtn.Click += (s, e) => p.EnterToSystem();
			RegisterBtn.Click += (s, e) => Task.Run(async () => await p.RegistrateInSystem());
			CloseBtn.Click += (s, e) => Application.Current.Shutdown();
		}


		/// <summary>
		/// Закрыть текущее окно, открыть окно загрузки
		/// </summary>
		/// <param name="worker"></param>
		private void OpenLoadWindow(IWorker worker)
		{
			this.Hide();
			LoadWindow window = new LoadWindow(worker as Worker);
			window.Show();
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
