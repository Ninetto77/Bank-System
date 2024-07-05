using Lesson10.activityLog;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using BankAccount_Library.account;
using BankAccount_Library.deposit;
using BankAccount_Library.currency;
using System.Linq.Expressions;

namespace Lesson10.MVP
{
    public class Presenter
    {
        public ActivityJournal journal;
        public Worker worker;
        public ObservableCollection<Department> Departments { get; set; }
        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<ICapital<BankAccount>> Accounts { get; set; }
        public List<string> Items { get; set; }


        private BankA bank;
        private IView view;

        #region Конструктор
        public Presenter(BankA bank, Worker worker, IView view)
        {
            this.bank = bank;
            this.worker = worker;
            this.view = view;

            Init();
        }
        public Presenter(Worker worker, IView view)
        {
            this.worker = worker;
            this.view = view;

            CreateBank();

            Init();
        }
        #endregion

        #region Инициализация
        private void Init()
        {
            CreateJournal();
            CreateCollections();

            InitCollections();
        }

        /// <summary>
        /// Создание банка
        /// </summary>
        private void CreateBank()
        {
            bank = new BankA("BankA", worker, worker.clients);
            InitAccountsList();
        }

        /// <summary>
        /// Создает список счетов
        /// </summary>
        private void InitAccountsList()
        {
            Accounts = new ObservableCollection<ICapital<BankAccount>>();
            foreach (var account in bank.AccountsDictionary.Keys)
            {
                Accounts.Add(account);
            }
        }

        /// <summary>
        /// init clients info items
        /// </summary>
        public void InitCollections()
        {
            Items = new List<string>
            {
                "Surname",
                "Name",
                "Middlename",
                "Phone",
                "PasportData"
            };

            UpdateClientList();
            Departments = worker.departmentsName;
        }


        private void CreateCollections()
        {
            Clients = new ObservableCollection<Client>();
            Departments = new ObservableCollection<Department>();
        }

        #endregion

        #region Обновление

        public void UpdateClientList()
        {
            Clients = worker.clients;
        }

        private void UpdateAccountsData()
        {
            view.AccountsData_IV = bank.FindClientAccounts(view.SelectedClient1_IV);
        }
        #endregion

        #region Журнал

        /// <summary>
        /// Создание журнала логов
        /// </summary>
        private void CreateJournal()
        {
            journal = new ActivityJournal();
            bank.Attach(journal);
            bank.Worker.Attach(journal);
        }

        #endregion

        #region События на кнопки

        #region События на кнопки вкладка клиент
        ///// <summary>
        ///// вопрос о добавление нового клиента
        ///// </summary>
        //public void AskToAddClient()
        //{
        //    if (worker.GetType() == typeof(Manager))
        //    {
        //        MessageBoxResult rezult = MessageBox.Show("Вы точно хотите добавить клиента?",
        //            "Вопрос",
        //            MessageBoxButton.YesNo,
        //            MessageBoxImage.Information);

        //        if (rezult == MessageBoxResult.Yes)
        //        {
        //            MainWindow SetWindow = Window.GetWindow(view as DependencyObject) as MainWindow;
        //            SetWindow.ChangeFrame("AddPage");
        //        }
        //    }
        //    else if (worker.GetType() == typeof(Consultate))
        //    {
        //        Post.PostErrorMessage("Недостаточно прав для добавления клиента");
        //    }

        //}

        /// <summary>
        /// добавление нового клиента
        /// </summary>
        public void AddClient(IViewAddPage viewAdd)
        {
            string id = viewAdd.SelectedDepartmentName_CB.DepartmentId.ToString();

            string surname = viewAdd.ClientSurname_TB;
            string name = viewAdd.ClientName_TB;
            string middlename = viewAdd.ClientMiddlename_TB;
            string phone = viewAdd.ClientPhone_TB;
            string pasportData = viewAdd.ClientPasport_TB;

            worker.AddClient(id, surname, name, middlename, phone, pasportData);
            
        }

        /// <summary>
        /// редактирование клиента
        /// </summary>
        public void EditClient()
        {
            Client client = view.SelectedClient_IV;
            if (client == null)
            {
                Post.PostErrorMessage($"Выберите чьи данные вы хотите изменить!");
                return;
            }
            AskForEditClient(client);
        }

        /// <summary>
        /// Сортировка клиентов
        /// </summary>
        public void SortClient()
        {
            worker.SortClientsBySurname();
            UpdateClientList();
            view.ClientData_IV = Clients.Where(c => int.Parse(c.DepartametId) == view.SelectedDepartmentName_CB.DepartmentId);
        }
        #endregion

        #region События на кнопки вкладка счет

        /// <summary>
        /// Удалить счет у клиента при нажатии на кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DeleteAccount()
        {
            if (IsEmptySelectedAccount_IV())
                return;

            bank.CloseClientAccount(view.SelectedAccount_IV as ICapital<BankAccount>);

            UpdateAccountsData();
            view.SecondAccountData_CB = bank.FindClientAccounts(view.SelectedClient1_IV);
        }

        /// <summary>
        ///  перевести средства между счетами у клиента при нажатии на кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TransferAccounts()
        {
            if (IsEmptySecondSelectedAccount_IV())
                return;
            if (IsEmptySelectedAccount_IV())
                return;
            float rubles = GetRuble();
            if (rubles == -1)
                return;

            ICapital<BankAccount> account1 = view.SelectedAccount_IV;
            ICapital<BankAccount> account2 = view.SecondSelectedAccount_CB;
            if (account1 == null || account2 == null)
            {
                Post.PostErrorMessage("Выберете счета, с которыми хотите работать!");
                return;
            }

            Ruble ruble = new Ruble(rubles);

            try
            {
                bank.TransferAccounts(account1, account2, ruble);
                //RefreshIvAccounts1();
            }
            catch
            {
                Post.PostMessage($"Произошла ошибка. Перевод не осуществлен");
            }
        }

        /// <summary>
        /// По нажатию кнопки Добавляет в счет средства
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddToAccountMoney()
        {
            if (IsEmptySelectedAccount_IV())
                return;
            float rubles = GetRuble();
            if (rubles == -1)
                return;

            bank.AddToAccountMoney(view.SelectedAccount_IV as ICapital<BankAccount>, rubles);
            //RefreshIvAccounts1();
            Post.PostMessage($"У {(view.SelectedAccount_IV as BankAccount).Name} счета всего {(view.SelectedAccount_IV as BankAccount).AccountRuble.Value} рублей");

        }

        /// <summary>
        /// По нажатию кнопки Снимает из счета средства
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SpendAccountMoney()
        {
            if (IsEmptySelectedAccount_IV())
                return;
            float rubles = GetRuble();
            if (rubles == -1)
                return;

            bank.SpendAccountMoney(view.SelectedAccount_IV as ICapital<BankAccount>, rubles);
            //RefreshIvAccounts1();

            Post.PostMessage($"У {(view.SelectedAccount_IV as BankAccount).Name} " +
                $"счета всего {(view.SelectedAccount_IV as BankAccount).AccountRuble.Value} рублей");
        }

        #endregion

        #region События на кнопки вкладка депозит
        /// <summary>
        /// Создает счет с капитализацией
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddWithCapital()
        {
            if (IsEmptySelectedClient1_IV())
                return;

            float rubles = GetRuble2();
            if (rubles == -1)
                AddAccountOfCurTypeWithoutRubles(CapitalEnum.accountWithCapitalization);
            else
            {
                Ruble ruble = new Ruble(rubles);
                AddAccountOfCurTypeWithRubles(CapitalEnum.accountWithCapitalization, ruble);
            }

            UpdateAccountsData();

            //RefreshIvAccounts1();
            //RefreshAccountComboBox1();
        }

        /// <summary>
        /// Создает счет без капитализации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddWithoutCapital()
        {
            if (IsEmptySelectedClient1_IV())
                return;
            float rubles = GetRuble2();
            if (rubles == -1)
            {
                AddAccountOfCurTypeWithoutRubles(CapitalEnum.accountWithoutCapitalization);
            }
            else
            {
                Ruble ruble = new Ruble(rubles);
                AddAccountOfCurTypeWithRubles(CapitalEnum.accountWithoutCapitalization, ruble);
            }

            UpdateAccountsData();

            // RefreshIvAccounts1();
            //RefreshAccountComboBox1();
        }

        /// <summary>
        /// Создает счет со взносом
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ruble"></param>
        private void AddAccountOfCurTypeWithRubles(CapitalEnum type, Ruble ruble)
        {
            bank.CreateAccountForClient(view.SelectedClient1_IV, type, ruble);
        }

        /// <summary>
        /// Создает счет без взноса
        /// </summary>
        /// <param name="type"></param>
        private void AddAccountOfCurTypeWithoutRubles(CapitalEnum type)
        {
            bank.CreateAccountForClient(view.SelectedClient1_IV, type);
        }
        /// <summary>
        /// Добавление средств на счет за год
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddMoneyForYear()
        {
            if (IsEmptySelectedAccount_IV())
                return;

            bank.AddPercentageForYear(view.SelectedAccount_IV);
            //RefreshIvAccounts1();
        }



        #endregion

        #region События на кнопки вкладка перевод
        /// <summary>
        ///  перевести средства между счетами двух клиентов при нажатии на кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TransferBetweenClientsCapital()
        {
            if (IsEmptySecondSelectedAccount3_IV())
                return;
            if (IsEmptySelectedAccount_IV())
                return;
            float rubles = GetRuble3();
            if (rubles == -1)
                return;

            ICapital<BankAccount> account1 = view.SelectedAccount_IV;
            ICapital<BankAccount> account2 = view.SecondSelectedAccount3_CB;

            if (account1 == null || account2 == null)
            {
                Post.PostErrorMessage("Выберете счета, с которыми хотите работать!");
                return;
            }

            Ruble ruble = new Ruble(rubles);

            try
            {
                bank.TransferAccountsBetweenClients(account1, account2, ruble);
                //RefreshIvAccounts1();
            }
            catch
            {
                Post.PostMessage($"Произошла ошибка. Перевод не осуществлен");
            }
        }



        #endregion

        #region errors
        private bool IsEmptySelectedAccount_IV()
        {
            if (view.SelectedAccount_IV == null)
            {
                Post.PostErrorMessage("Выберете аккаунт справа!");
                return true;
            }
            return false;
        }
        private bool IsEmptySelectedClient_IV()
        {
            if (view.SelectedClient_IV == null)
            {
                Post.PostErrorMessage("Выберете клиента справа!");
                return true;
            }
            return false;
        }
        private bool IsEmptySelectedClient1_IV()
        {
            if (view.SelectedClient1_IV == null)
            {
                Post.PostErrorMessage("Выберете клиента справа!");
                return true;
            }
            return false;
        }
        private bool IsEmptySecondSelectedAccount_IV()
        {
            if (view.SecondSelectedAccount_CB == null)
            {
                Post.PostErrorMessage("Выберете аккаунт в списке!");
                return true;
            }
            return false;
        }
        private bool IsEmptySecondSelectedAccount3_IV()
        {
            if (view.SecondSelectedAccount3_CB == null)
            {
                Post.PostErrorMessage("Выберете аккаунт в списке!");
                return true;
            }
            return false;
        }
        private float GetRuble()
        {
            if (view.Rubles_TB == null)
            {
                Post.PostErrorMessage("");
                return -1;
            }
            float rubles = -1;
            if (float.TryParse(view.Rubles_TB, out rubles))
            {
                if (rubles < 0)
                {
                    return -1;
                }
                return rubles;
            }
            return -1;
        }
        private float GetRuble2()
        {
            if (view.Rubles2_TB == null)
            {
                Post.PostErrorMessage("");
                return -1;
            }
            float rubles = -1;
            if (float.TryParse(view.Rubles2_TB, out rubles))
            {
                if (rubles < 0)
                {
                    return -1;
                }
                return rubles;
            }
            return -1;
        }
        private float GetRuble3()
        {
            if (view.Rubles3_TB == null)
            {
                Post.PostErrorMessage("");
                return -1;
            }
            float rubles = -1;
            if (float.TryParse(view.Rubles3_TB, out rubles))
            {
                if (rubles < 0)
                {
                    return -1;
                }
                return rubles;
            }
            return -1;
        }
        #endregion

        #endregion

        #region 
        #endregion

        #region Работа с клиентом
        /// <summary>
        /// окно на потверждение изменения клиента
        /// </summary>
        /// <param name="client"></param>
        private void AskForEditClient(Client client)
        {
            MessageBoxResult rezult = MessageBox.Show($"Вы уверены, что хотите отредактировать у" +
                $" {client.Name} изменить {view.SelectedItem_CB} на {view.ChangedClientData_TB}?",
                "Вопрос",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (rezult == MessageBoxResult.Yes)
                EditClient(client);
        }
        /// <summary>
        /// Изменение клиента
        /// </summary>
        /// <param name="client"></param>
        private void EditClient(Client client)
        {
            int index = view.SelectedItemIndex_CM;
            string Name = client.Name;

            switch (index)
            {
                case 0:
                    worker.ChangeClientSurname(Name, view.ChangedClientData_TB);
                    break;
                case 1:
                    worker.ChangeClientName(Name, view.ChangedClientData_TB);
                    break;
                case 2:
                    worker.ChangeClientMiddlename(Name, view.ChangedClientData_TB);
                    break;
                case 3:
                    worker.ChangePhone(Name, view.ChangedClientData_TB);
                    break;
                case 4:
                    worker.ChangePasportData(Name, view.ChangedClientData_TB);
                    break;
            }
        }

        #endregion

        #region Работа с банком
        public void FindClientAccounts(Client client)
        {
            if (client == null) return;
            List<ICapital<BankAccount>> list = bank.FindClientAccounts(client);
            Accounts =new  ObservableCollection<ICapital<BankAccount>>(list);

            view.AccountsData_IV = Accounts;
            view.SecondAccountData_CB = Accounts;
        }
        public IEnumerable<ICapital<BankAccount>> GetClientAccounts(Client client)
        {
            if (client == null) return null;

            List<ICapital<BankAccount>> list = bank.FindClientAccounts(client);
            Accounts = new ObservableCollection<ICapital<BankAccount>>(list);

            return Accounts;
        }

        #endregion

        #region
        #endregion

    }
}
