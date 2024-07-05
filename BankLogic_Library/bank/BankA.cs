using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Lesson10.activityLog;
using Lesson10.observer;
using Lesson10.transaction;
using Lesson10.utills;

using BankAccount_Library.account;
using BankAccount_Library.deposit;
using BankAccount_Library.currency;

namespace Lesson10
{
    public class BankA: INotifyPropertyChanged, ISubject
    {
        public event Action<string> OnOpeningAccount;
        public event Action<string> OnClosingAccount;
        public event Action<string> OnTransferAccounts;

        public event Action<BankAccount, TypeOfAct> OnAnyChange;
        
        public List<IObserver> observers = new List<IObserver>();

        private const string openAccountString = "Новый счет открыт!";
        private const string closeAccountString = "Выбранный счет закрыт!";
        private const string transferAccountsString = "Перевод успешно выполнен!";

        private const string filePathAccounts = "accountsdb.xml";
        private byte percent = 12;


        #region Свойства
        public string Name { get; private set; }

        public IWorker Worker { get; private set; }


        private Dictionary<ICapital<BankAccount>, Client> accountsDictionary;
        public Dictionary<ICapital<BankAccount>, Client> AccountsDictionary
        {
            get { return accountsDictionary; }
            set
            {
                accountsDictionary = value;
                OnPropertyChanged("AccountsDictionary");
            }
        }

        #endregion

        #region Конструкторы
        public BankA(string name, IWorker worker)
        {
            Name = name;
            SetWorker(worker);
            CreateActivityLogList();
            CreateDictionary();
        }
        public BankA(string name, IWorker worker, ObservableCollection<Client> clients)
        {
            Name = name;
            SetWorker(worker);
            CreateActivityLogList();
            CreateDictionary();
            CreateAccountsForClients(clients);
        }


        #endregion

        #region Методы

        #region Создание счетов

        /// <summary>
        /// Создает для списка клиентов по одному счету
        /// </summary>
        /// <param name="clients"></param>
        public void CreateAccountsForClients(ObservableCollection<Client> clients)
        {
            foreach (var client in clients)
            {
                CreateAccountForClient(client, CapitalEnum.accountWithCapitalization);
            }
           // SaveAccountsInFile();
        }

        /// <summary>
        /// Создает для клиента счет
        /// </summary>
        /// <param name="client">Клиент, у которого создается счет</param>
        public void CreateAccountForClient(Client client, CapitalEnum typeOfCapital)
        {
            ICapital<BankAccount> account = null;
            if (typeOfCapital.Equals(CapitalEnum.accountWithCapitalization) == true)
                account = new AccountWithCapitalization();
            if (typeOfCapital.Equals(CapitalEnum.accountWithoutCapitalization) == true)
                account = new AccountWithoutCapitalization();

            //OnAnyChange?.Invoke(account as BankAccount, TypeOfAct.openAccount);
            FinishCreationAccount(account, client);
        }

        /// <summary>
        /// Создает для клиента счет
        /// </summary>
        /// <param name="client">Клиент, у которого создается счет</param>
        public void CreateAccountForClient(Client client, CapitalEnum typeOfCapital, Ruble ruble)
        {
            ICapital<BankAccount> account = null;
            if (typeOfCapital.Equals(CapitalEnum.accountWithCapitalization) == true)
                account = new AccountWithCapitalization(ruble);
            if (typeOfCapital.Equals(CapitalEnum.accountWithoutCapitalization) == true)
                account = new AccountWithoutCapitalization(ruble);

            //OnAnyChange?.Invoke(account as BankAccount, TypeOfAct.openAccount);
            FinishCreationAccount(account, client);
        }

        /// <summary>
        /// Создание словаря из счета и клиента
        /// </summary>
        public void CreateDictionary()
        {
            if (AccountsDictionary == null)
            {
                AccountsDictionary = new Dictionary<ICapital<BankAccount>, Client>();
            }
        }

        /// <summary>
        /// Создание журнала изменений
        /// </summary>
        private void CreateActivityLogList()
        {
            OnAnyChange += AddToActivityLog;
        }

        /// <summary>
        /// Подписка на все события
        /// </summary>
        /// <param name="account"></param>
        private void DicribeOnEvent(ICapital<BankAccount> account)
        {
            (account as BankAccount).OnChangedAccountRubles += Post.PostMessage;
            //OnOpeningAccount += Post.PostMessage;
            //this.OnClosingAccount += Post.PostMessage;
            //OnTransferAccounts += Post.PostMessage;
        }

        /// <summary>
        /// Заканчивает открытие счета
        /// </summary>
        /// <param name="account"></param>
        /// <param name="client"></param>
        private void FinishCreationAccount(ICapital<BankAccount> account, Client client)
        {
            DicribeOnEvent(account);

           // OnOpeningAccount?.Invoke(openAccountString);
            AccountsDictionary.Add(account, client);
            UpdateDictinary();
        }


        #endregion

        #region Действия со счетами

        /// <summary>
        /// Поиск по имени клиента его счеты
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public List<ICapital<BankAccount>> FindClientAccounts(Client client)
        {
            if (client == null)
                return null;

            List<ICapital<BankAccount>> temp = new List<ICapital<BankAccount>>();

            foreach (var account in AccountsDictionary)
            {
                if (account.Value == client)
                    temp.Add(account.Key);
            }

            if (temp.Count <= 0)
            {
                Post.PostMessage($"У клиента {client.Name} нет ни одного счета");
            }
            return temp;
        }

        /// <summary>
        /// Поиск имени клиента по его счету
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public Client FindClientByAccount(BankAccount account)
        {
            if (account == null)
                return null;

            List<ICapital<BankAccount>> temps = new List<ICapital<BankAccount>>();

            foreach (var temp in AccountsDictionary)
            {
                if (temp.Key == account)
                    return temp.Value;
            }

           return null;
        }

        /// <summary>
        /// Удаление у клиента счета
        /// </summary>
        /// <param name="clients"></param>
        public void CloseClientAccount(ICapital<BankAccount> account)
        {
            if (AccountsDictionary.ContainsKey(account))
            {
                OnAnyChange?.Invoke(account as BankAccount, TypeOfAct.closeAccount);

                AccountsDictionary.Remove(account);
                Post.PostMessage("Удаление счета " + (account as BankAccount).Name);
                OnClosingAccount?.Invoke(closeAccountString);
                UpdateDictinary();

            }
        }

        /// <summary>
        /// Перевод между счета клиента
        /// </summary>
        /// <param name="account1"></param>
        /// <param name="account2"></param>
        /// <param name="rubles"></param>
        public void TransferAccounts(ICapital<BankAccount> account1, ICapital<BankAccount> account2, Ruble rubles)
        {
            if (account1 == account2)
            {
                Post.PostMessage($"Выберете разные счета!");
                return;
            }
            if (AccountsDictionary[account1].Name == AccountsDictionary[account2].Name)
                if ((account1 as BankAccount).CanSpendMoney(rubles))
                {
                    (account1 as BankAccount).SpendMoney(rubles);
                    (account2 as BankAccount).AddMoney(rubles);
                    OnTransferAccounts?.Invoke(transferAccountsString);

                    OnAnyChange?.Invoke(account1 as BankAccount, TypeOfAct.transferMoneyAccount);
                }
                else
                    Post.PostMessage($"У счета {account1.Name} недостаточно средств");
            else
                Post.PostMessage($"У счетов {account1.Name} и {account2.Name} разные владельцы");
        }

        /// <summary>
        /// Перевод между счета между клиентами
        /// </summary>
        /// <param name="account1"></param>
        /// <param name="account2"></param>
        /// <param name="rubles"></param>
        public void TransferAccountsBetweenClients(ICapital<BankAccount> account1, ICapital<BankAccount> account2, Ruble rubles)
        {
            ITransaction<BankA> transaction = new TransactionBank();
            transaction.TransferAccountsBetweenClients(account1, account2, rubles);

            OnAnyChange?.Invoke(account1 as BankAccount, TypeOfAct.transferBetweenClientsMoney);
        }

        /// <summary>
        /// Добавляет в счет средства
        /// </summary>
        /// <param name="account"></param>
        /// <param name="value"></param>
        public void AddToAccountMoney(ICapital<BankAccount> account, float value)
        {
            Ruble rubles = new Ruble(value);
            (account as BankAccount).AddMoney(rubles);
            OnAnyChange?.Invoke(account as BankAccount, TypeOfAct.addMoneyAccount);
        }
        /// <summary>
        /// Снимает со счета средства
        /// </summary>
        /// <param name="account"></param>
        /// <param name="value"></param>
        public void SpendAccountMoney(ICapital<BankAccount> account, float value)
        {
            Ruble rubles = new Ruble(value);
            (account as BankAccount).SpendMoney(rubles);
            OnAnyChange?.Invoke(account as BankAccount, TypeOfAct.spendMoneyAccount);
        }

        #endregion

        #region Действия с капиталом

        /// <summary>
        /// Добавить проценты за год
        /// </summary>
        /// <param name="account"></param>
        public void AddPercentageForYear(ICapital<BankAccount> account)
        {
            if ((account as AccountWithCapitalization) != null)
                (account as AccountWithCapitalization).AddPercentageForYear(percent);
            else
            if ((account as AccountWithoutCapitalization) != null)
                (account as AccountWithoutCapitalization).AddPercentageForYear(percent);
        }

        #endregion

        #region Журнал изменений

        /// <summary>
        /// Добавить запись в журнал изменений
        /// </summary>
        /// <param name="account"></param>
        /// <param name="type"></param>
        private void AddToActivityLog(BankAccount account, TypeOfAct type)
        {
            Client client = FindClientByAccount(account);

            if (client == null)
            {
                Post.PostMessage("Ошибка");
                return;
            }

            Notify(client, type, account);
        }

        /// <summary>
        /// Добавить запись в журнал изменений
        /// </summary>
        /// <param name="client"></param>
        /// <param name="account"></param>
        /// <param name="type"></param>
        private void AddToActivityLog(Client client, BankAccount account, TypeOfAct type)
        {
            Notify(client, type, account);
        }

        /// <summary>
        /// Добавить запись в журнал изменений
        /// </summary>
        /// <param name="client"></param>
        /// <param name="account"></param>
        /// <param name="type"></param>
        private void AddToActivityLog(Client client, TypeOfAct type)
        {
            BankAccount account = null;
            Notify(client, type, account);
        }

        #endregion

        #region Другое

        /// <summary>
        /// метод для того, чтобы свойство set у dictinary сработал
        /// </summary>
        private void UpdateDictinary()
        {
            Dictionary<ICapital<BankAccount>, Client> temp = AccountsDictionary;
            AccountsDictionary = temp;
        }

        /// <summary>
        /// Устанавливает работника, подписывается на события
        /// </summary>
        /// <param name="worker"></param>
        private void SetWorker(IWorker worker)
        {
            Worker = worker;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
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


        #endregion

    }
}
