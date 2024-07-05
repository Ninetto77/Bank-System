using Lesson10.activityLog;
using Lesson10.observer;
using System;

namespace Lesson10
{
    public interface IWorker: ISubject
    {
        /// <summary>
        /// Событие изменения данных о клиенте
        /// </summary>
        event Action<Client, TypeOfAct> OnChangeClient;

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

        void ShowClientData();
        void AddClient(string id, string surname, string name,
                                      string middlename, string phone,
                                      string pasportData);
    }
}
