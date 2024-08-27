namespace BankLogic_Library.MVP
{
	public interface IViewAuthWindow
	{
		string Login { get; }
		string Password { get; }
		bool? isManager { get; }
	}
}
