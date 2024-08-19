using Lesson10.activityLog;
using Lesson10.observer;
using System;
using System.Threading.Tasks;

namespace Lesson10
{
    public interface IWorker: ISubject
    {
        /// <summary>
        /// Событие изменения данных о клиенте
        /// </summary>
        event Action<Client, TypeOfAct> OnChangeClient;

        /// <summary>
        /// Дожидается загрузки клиетов из файла 
        /// </summary>
        /// <returns></returns>
        Task UploadClientsFromXMLAsync();

        /// <summary>
        /// Изменение  фамилии
        /// </summary>
        /// <param name="surname">Имя клиента, у которого изменить данные</param>
        /// <param name="changedSurname">Новая фамилия</param>
        void ChangeClientSurname(string surname, string changedSurname);
        /// <summary>
        /// Изменение имени
        /// </summary>
        /// <param name="name">Имя клиента, у которого изменить данные</param>
        /// <param name="changedName">Новое имя</param>
        void ChangeClientName(string name, string changedName);
        /// <summary>
        /// Изменение отечества
        /// </summary>
        /// <param name="Middlename">Имя клиента, у которого изменить данные</param>
        /// <param name="changedMiddlename">Новое отечество</param>
        void ChangeClientMiddlename(string Middlename, string changedMiddlename);
        /// <summary>
        /// Изменение телефона
        /// </summary>
        /// <param name="name">Имя клиента, у которого изменить данные</param>
        /// <param name="phone">Новый номер телефона</param>
        void ChangePhone(string name, string phone);
        /// <summary>
        /// Изменение данных паспорта
        /// </summary>
        /// <param name="name">Имя клиента, у которого изменить данные</param>
        /// <param name="changedData">Новые паспортные данные</param>
        void ChangePasportData(string name, string changedData);
        /// <summary>
        /// Добавить клиента
        /// </summary>
        /// <param name="id"></param>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="middlename"></param>
        /// <param name="phone"></param>
        /// <param name="pasportData"></param>
        void AddClient(string id, string surname, string name,
                                      string middlename, string phone,
                                      string pasportData);
    }
}
