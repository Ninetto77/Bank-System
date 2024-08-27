using System.Threading.Tasks;

namespace BankLogic_Library.Authorization
{
	public interface IAthorization
	{
		/// <summary>
		/// регистрация пользователя
		/// </summary>
		/// <returns></returns>
		Task RegisterWorker();
		/// <summary>
		/// логирование пользователя
		/// </summary>
		/// <returns></returns>
		Task LoginWorker();
	}
}
