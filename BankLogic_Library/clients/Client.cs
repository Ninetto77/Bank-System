using Lesson10.activityLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lesson10
{
    public class Client: INotifyPropertyChanged, IComparable<Client>
    {
        static Random r;

        public event Action<Client, TypeOfAct> OnAnyChange;

        private int clientId;
        private string departametId;
        private string surname;
        private string name;
        private string middlename;
        private string phone;
        private string pasportData;

        #region Свойства
        public int ClientId
        {
            get { return clientId; }
            set
            {
                clientId = value;
                OnPropertyChanged("ClientId");
            }
        }
        public string DepartametId
        {
            get { return departametId; }
            set
            {
                departametId = value;
                OnPropertyChanged("DepartametId");
            }
        }
        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Middlename
        {
            get { return middlename; }
            set
            {
                middlename = value;
                OnPropertyChanged("Middlename");
            }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }

        public string PasportData
        {
            get { return pasportData; }
            set
            {
                pasportData = value;
                OnPropertyChanged("PasportData");
            }
        }
        #endregion

        private string dateOfChangedData { get; set; }

        private List<string> changedDataList = new List<string>();
        private List<string> typeOfChangedDataList = new List<string>();
        private List<string> PersonChangesDataList = new List<string>();

        public enum WorkerEnum
        {
            consultate,
            manager,
        }


        public enum ChangedData
        {
            DepartmentID,
            Surname,
            Name,
            Middlename,
            Phone,
            PasportData
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        #region Конструкторы
        static Client()
        {
            r = new Random();
        }

        public Client()
        {

        }

        public Client(
                        string DepartmentID,
                        string Surname,
                        string Name,
                        string Middlename,
                        string Phone,
                        string PasportData)

        {
            this.DepartametId = DepartmentID;
            this.Surname = Surname;
            this.Name = Name;
            this.Middlename = Middlename;
            this.Phone = Phone;
            this.PasportData = PasportData;
        }

        public Client(
                string Surname,
                string Name,
                string Middlename,
                string Phone,
                string PasportData)

        {
            this.DepartametId = (r.Next(1, 4)).ToString();
            this.Surname = Surname;
            this.Name = Name;
            this.Middlename = Middlename;
            this.Phone = Phone;
            this.PasportData = PasportData;
        }

        public Client(
                        string DepartmentID,
                        string Surname,
                        string name,
                        string Phone)

        {
            this.DepartametId = DepartmentID;
            this.Surname = Surname;
            this.Name = name;
            this.Middlename = "Отсутствует";
            this.Phone = Phone;
            this.PasportData = "";
        }

        #endregion

        #region
        private void InvokeOnChange()
        {
            OnAnyChange?.Invoke(this, TypeOfAct.changeClientData);
        }

        #endregion

        override public string ToString()
        {
            return $"{Surname,10}{Name,10}{Middlename,10}{Phone,6}{PasportData,10}";
        }


        public void AddChangedData(ChangedData data, WorkerEnum worker)
        {
            dateOfChangedData = DateTime.Now.ToShortDateString();
            switch (data)
            {
                case ChangedData.Surname:
                    changedDataList.Add("Surname");
                    break;
                case ChangedData.Name:
                    changedDataList.Add("Name");
                    break;
                case ChangedData.Middlename:
                    changedDataList.Add("Middlename");
                    break;
                case ChangedData.Phone:
                    changedDataList.Add("Phone");
                    break;
                case ChangedData.PasportData:
                    changedDataList.Add("PasportData");
                    break;
            }
            typeOfChangedDataList.Add("Исправление");

            InvokeOnChange();

            if (worker == WorkerEnum.consultate)
                PersonChangesDataList.Add("Консультант");
            else
                PersonChangesDataList.Add("Менеджер");

        }

        public int CompareTo(Client other)
        {
            int rezul = String.Compare(this.Surname, other.Surname);
            if ( rezul < 0)
                return -1;
            else 
                if (rezul > 0) return 1;
                else
                    return 0;
        }
    }
}
