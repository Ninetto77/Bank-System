using System.Threading.Tasks;
using System.Windows;
using BankLogic_Library.MVP;
using BankLogic_Library.MVP.Presenters;

namespace Lesson10
{
	/// <summary>
	/// Логика взаимодействия для AuthorizationWindow.xaml
	/// </summary>
	public partial class AuthorizationWindow : Window, IViewAuthWindow
	{
		public string Login { get => LoginTextBox.Text; }
		public string Password { get => PasswordTextBox.Text; }
		public bool? isManager { get => WorkerCheckBox.IsChecked; }


		private AuthPresenter p;

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
		}

		/// <summary>
		/// Закрыть текущее окно, открыть окно загрузки
		/// </summary>
		/// <param name="worker"></param>
		private void OpenLoadWindow(Worker worker)
		{
			this.Hide();
			LoadWindow window = new LoadWindow(worker);
			window.Show();
		}


	}
}
