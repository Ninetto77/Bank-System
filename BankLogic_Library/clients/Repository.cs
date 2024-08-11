using BankLogic_Library.DB;
using Lesson10.utills;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lesson10
{
    public class Repository
    {
        static Random r;
        public ObservableCollection<Department> Departments { get; set; }
        public ObservableCollection<Client> Clients { get; set; }

        ClientContex db;

        private const string filePath = "clientdb.xml";

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

        public void CreateClients(int countOfClients)
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

            SaveInFile();
        }

        public void SaveInFile()
        {
            XMALWork.SerializeFields(Clients, filePath);

        }
        public async Task GetFromFileAsync()
        {
            var task = XMALWork.DeserializeField<Client>(filePath);
            await task;

            Clients = task.Result as ObservableCollection<Client>;

            if (Clients == null)
            {
                Post.PostMessage("Нет клиентов");
            }
        }
    }
}
