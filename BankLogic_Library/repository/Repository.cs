using BankLogic_Library.DB;
using Lesson10.utills;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Lesson10.repository
{
    public abstract class Repository: IRepository
    {
        static Random r;
        public ObservableCollection<Department> Departments { get; set; }
        public ObservableCollection<Client> Clients { get; set; }

        ClientContex db;


        static Repository()
        {
            r = new Random();
        }

        public Repository(int countOfDepartments, int countOfClients)
        {
            Departments = new ObservableCollection<Department>();
            Clients = new ObservableCollection<Client>();

            for (int i = 0; i < countOfDepartments; i++)
            {
                Departments.Add(new Department($"Department_{i + 1}", i + 1));
            }

            //CreateClients(countOfClients);
		}

        public virtual void CreateClients(int countOfClients)  
        {
			for (int i = 0; i < 1000000; i++)
			{

				Clients.Add(new Client(
					$"{(r.Next(Departments.Count + 1))}",
					$"Surname_{i + 1}",
					$"Name_{i + 1}",
					$"MiddleName_{i + 1}",
					"79999999999",
					$"6666664444"
					));
			}
		}

        public virtual void SaveClientContext() { return; }

		public virtual Task GetClientContextAsync() => Task.Delay(1000);
	}
}
