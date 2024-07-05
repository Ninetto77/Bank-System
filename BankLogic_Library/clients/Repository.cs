using BankLogic_Library.DB;
using Lesson10.utills;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Lesson10
{
    public class Repository
    {
        static Random r;
        public ObservableCollection<Department> Departments { get; set; }
        public IEnumerable<Client> Clients { get; set; }

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
            db = new ClientContex();


            for (int i =0; i < countOfDepartments; i++)
            {
                Departments.Add(new Department($"Department_{i+1}", i+1));
            }

            //CreateClients(countOfClients);
            GetFromFile();
        }

        public void CreateClients(int countOfClients)
        {
            for (int i = 0; i < countOfClients; i++)
            {
                db.Clients.Add(new Client(
                    $"{(r.Next(Departments.Count + 1))}",
                    $"Surname_{i + 1}",
                    $"Name_{i + 1}",
                    $"MiddleName_{i + 1}",
                    "79999999999",
                    $"{i + 1}{i + 1}{i + 1}{i + 1}{i + 1}{i + 1}{i + 1}{i + 1}{i + 1}{i + 1}"
                    ));

                Clients = db.Clients;

                //foreach (var client in db.Clients)
                //{
                //    Clients.Add(client);
                //}
                //Clients.Add(new Client(
                //    $"{(r.Next(Departments.Count + 1))}",
                //    $"Surname_{i + 1}",
                //    $"Name_{i + 1}",
                //    $"MiddleName_{i + 1}",
                //    "79999999999",
                //    $"{i + 1}{i + 1}{i + 1}{i + 1}{i + 1}{i + 1}{i + 1}{i + 1}{i + 1}{i + 1}"
                //    ));
            }


            db.SaveChanges();
            //SaveInFile();
        }

        public void GetFromDB()
        {
            var t = db.Clients;
            Clients = t;
        }

        public void SaveInFile()
        {
            XMALWork.SerializeFields(Clients, filePath);

        }
        public void GetFromFile()
        {
            Clients = XMALWork.DeserializeField<Client>(filePath);
        }

    }
}
