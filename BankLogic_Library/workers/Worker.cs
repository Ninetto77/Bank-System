using Lesson10.activityLog;
using Lesson10.observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using BankAccount_Library.account;
using System.Threading.Tasks;

namespace Lesson10
{
    public abstract class Worker : IWorker
    {
        #region Поля
        public event Action<Client, TypeOfAct> OnChangeClient;

        public List<string> items;
        public ObservableCollection<Department> departmentsName;

        public ObservableCollection<Client> clients;

        public List<IObserver> observers = new List<IObserver>();

        private readonly int countOfDepartmentsDefault = 3;
        private readonly int countOfClientsDefault = 15;

		private int countOfDepartments;
		private int countOfClients;

		#endregion

		#region Конструкторы
		public Worker(int countOfDepartments, int countOfClients)
        {
            this.countOfDepartments= countOfDepartments;
            this.countOfClients= countOfClients;

			//CreateRepository(countOfDepartments, countOfClients);
        }
        public Worker()
        {
			this.countOfDepartmentsDefault = 3;
			this.countOfClientsDefault = 15;

            this.countOfDepartments = countOfDepartmentsDefault;
			this.countOfClients = countOfClientsDefault;

			//CreateRepository(countOfDepartmentsDefault, countOfClientsDefault);
        }

        #endregion


        #region Перегрузки
        /// <summary>
        /// Возвращает экземпляр Client или null, если такого экземпляра нет
        /// </summary>
        /// <param name="index">Позиция в базе данных</param>
        /// <returns></returns>
        public Client this[int index]
        {
            get { return clients[index]; }
        }
        /// <summary>
        /// Возвращает экземпляр Client или null, если такого экземпляра нет
        /// </summary>
        /// <param name="surname">Фамилия клиента</param>
        /// <returns></returns>
        public Client this[string surname]
        {
            get 
            {
                Client client = null;
                foreach (var e in this.clients)
                {
                    if (e.Surname == surname)
                    {
                        client = e;
                        break;
                    }
                }
                return client;
            }
        }
        #endregion


        #region Functions

        /// <summary>
        /// init depatrtment names items
        /// </summary>
        public void InitDepartmentItems(Repository repository)
        {
            departmentsName = new ObservableCollection<Department>();
            ObservableCollection<Department> temp = repository.Departments;
           
            departmentsName = temp;
        }

        public async Task UploadClientsFromFileAsync()
        {
            await CreateRepositoryAsync(countOfDepartments, countOfClients);
        }

        /// <summary>
        /// Create repository
        /// </summary>
        /// <param name="countOfDepartments">count of departaments</param>
        /// <param name="countOfClients">common count of clients</param>
        private async Task CreateRepositoryAsync(int countOfDepartments, int countOfClients)
        {
            Repository repository = new Repository(countOfDepartments, countOfClients);
            await repository.GetFromFileAsync();

            clients = repository.Clients as ObservableCollection<Client>;

            InitDepartmentItems(repository);
            DescribeOnClientChangeEvent();
        }

        private void DescribeOnClientChangeEvent()
        {
            foreach (var client in clients)
            {
                client.OnAnyChange += OnClientChange;
            }
        }

        private void OnClientChange(Client client, TypeOfAct act)
        {
            Post.PostMessage("Изменение клиента");
            Notify(client, act);
        }


        public virtual void ChangeClientSurname(string surname, string changedSurname)
        {
            MessageBox.Show($"Недостаточно прав для редактирования");
        }
        public virtual void ChangeClientName(string name, string changedName)
        {
            MessageBox.Show($"Недостаточно прав для редактирования");

        }
        public virtual void ChangeClientMiddlename(string Middlename, string changedMiddlename)
        {
            MessageBox.Show($"Недостаточно прав для редактирования");

        }
        public virtual void ChangePhone(string name, string phone)
        {
            MessageBox.Show($"Недостаточно прав для редактирования");

        }
        public virtual void ChangePasportData(string name, string changedData)
        {
            MessageBox.Show($"Недостаточно прав для редактирования");

        }
        public virtual void ShowClientData()
        {
            Console.WriteLine($"Surname\tName\tMiddlename\tPhone\tPasportData");
            foreach (Client client in clients)
            {
                Console.Write(
                    $"{client.Surname}\t" +
                    $"{client.Name}\t" +
                    $"{client.Middlename}\t" +
                    $"{client.Phone}\t");

                if (client.PasportData != "")
                    Console.WriteLine("******************");
            }
        }



        public virtual void AddClient(string id, string surname, string name,
                      string middlename, string phone,
                      string pasportData)
        {
            Console.WriteLine("Недостаточно прав для редактирования");
        }

        /// <summary>
        /// Sort clients by surname
        /// </summary>
        public virtual void SortClientsBySurname()
        {
            List<Client> sortedList = clients.ToList();
            sortedList.Sort();

            clients.Clear();

            foreach (Client item in sortedList)
            {
                clients.Add(item);
            }
        }


        #region ISubject
        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(Client client, TypeOfAct type, BankAccount account = null)
        {
            foreach (var obs in observers)
            {
                obs.Update(client, type, account);
            }
        }

		#endregion

		#endregion
	}
}
