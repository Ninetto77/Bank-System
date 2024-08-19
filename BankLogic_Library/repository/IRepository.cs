using System.Threading.Tasks;

namespace Lesson10.repository
{
	public interface IRepository
	{
		/// <summary>
		/// Создание клиентов
		/// </summary>
		/// <param name="countOfClients"></param>
		void CreateClients(int countOfClients);
		/// <summary>
		/// Сохранение базы клиентов
		/// </summary>
		void SaveClientContext();
		/// <summary>
		/// Извлечение базы клиентов
		/// </summary>
		/// <returns></returns>
		Task GetClientContextAsync();
	}
}
