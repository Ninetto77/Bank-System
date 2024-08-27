using Lesson10.MVP;
using System.Windows;
using BankLogic_Library.MVP;

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


		private Presenter p;
		public AuthorizationWindow()
		{
			p = new Presenter(this);
			InitializeComponent();
			InitButtons();
		}


		private void InitButtons()
		{
			EnterBtn.Click += (s, e) => p.EnterToSystem();
			RegisterBtn.Click += (s, e) => p.RegistrateInSystem();
		}

	}
}
