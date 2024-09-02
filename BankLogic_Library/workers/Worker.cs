using Lesson10.activityLog;
using Lesson10.observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using BankAccount_Library.account;
using System.Threading.Tasks;
using Lesson10.repository;
using BankLogic_Library.repository;
using BankLogic_Library.DB;


namespace Lesson10
{
    public abstract class Worker : IWorker
    {
        #region Поля
        public event Action<Clients, TypeOfAct> OnChangeClient;

        public List<string> items;
        public ObservableCollection<Department> departmentsName;

        public ObservableCollection<Clients> clients;

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
        public Clients this[int index]
        {
            get { return clients[index]; }
        }
        /// <summary>
        /// Возвращает экземпляр Client или null, если такого экземпляра нет
        /// </summary>
        /// <param name="surname">Фамилия клиента</param>
        /// <returns></returns>
        public Clients this[string surname]
        {
            get 
            {
                Clients client = null;
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
        public void InitDepartmentItems(IRepository repository)
        {
            departmentsName = new ObservableCollection<Department>();
            ObservableCollection<Department> temp = (repository as Repository).Departments;
           
            departmentsName = temp;
        }

        public async Task UploadClientsFromXMLAsync()
        {
            await CreateRepositoryAsync(countOfDepartments, countOfClients, "XML");
        }

		public async Task UploadClientsFromDBAsync()
		{
			await CreateRepositoryAsync(countOfDepartments, countOfClients, "DB");
		}

		/// <summary>
		/// Create repository
		/// </summary>
		/// <param name="countOfDepartments">count of departaments</param>
		/// <param name="countOfClients">common count of clients</param>
		private async Task CreateRepositoryAsync(int countOfDepartments, int countOfClients, string place)
        {
            Repository repository = null;
            if (place == "XML")
                repository = new XMLRepository(countOfDepartments, countOfClients);
            else
			    if (place == "DB")
				    repository = new DBRepository(countOfDepartments, countOfClients);

			await repository.GetClientContextAsync();

            clients = repository.Clients;

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

        private void OnClientChange(Clients client, TypeOfAct act)
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
            foreach (Clients client in clients)
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
            List<Clients> sortedList = clients.ToList();
            sortedList.Sort();

            clients.Clear();

            foreach (Clients item in sortedList)
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

        public void Notify(Clients client, TypeOfAct type, BankAccount account = null)
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
