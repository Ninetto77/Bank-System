using BankLogic_Library.DB;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lesson10.repository
{
    public abstract class Repository: IRepository
    {
        static Random r;
        public ObservableCollection<Department> Departments { get; set; }
        public ObservableCollection<Clients> Clients { get; set; }

        static Repository()
        {
            r = new Random();
        }

        public Repository(int countOfDepartments, int countOfClients)
        {
            Departments = new ObservableCollection<Department>();
            Clients = new ObservableCollection<Clients>();

            for (int i = 0; i < countOfDepartments; i++)
            {
                Departments.Add(new Department($"Department_{i + 1}", i + 1));
            }

           // CreateClients(countOfClients);
		}

        public virtual void CreateClients(int countOfClients)  
        {
			for (int i = 0; i < 100; i++)
			{

				Clients.Add(new Clients(
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
