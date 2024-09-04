using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BankAccount_Library.currency;


namespace BankAccount_Library.account
{
    public abstract class BankAccount: INotifyPropertyChanged
    {
        #region Поля
        public event Action<string> OnChangedAccountRubles;
        

        private const string addSuccesAccountRubleString = "Средства на счете успешно добавлены!";
        private const string spendSuccesAccountRubleString = "Средства на счете успешно сняты!";
        private const string spendFaildAccountRubleString = "Не удалось взять деньги из счета";
        #endregion

        #region Свойства
        public static int id = 0;
        public int ID { get; private set; }
        public string Name { get; private set; }
        
        public Ruble accountRuble;
        public Ruble AccountRuble { 
            get=> accountRuble; 
            set
            {
                this.accountRuble = value;
                OnPropertyChanged(nameof(AccountRuble));
            } }

        #endregion

        #region Конструкторы
        static BankAccount()
        {
            id = 0;
        }
        public BankAccount()
        {
            ID = GetID();
            Name = $"AccountName_{ID}";
            AccountRuble = new Ruble();
        }
        public BankAccount(Ruble rubles) : this()
        {
            AccountRuble.Add(rubles.Value) ;
        }
        public BankAccount(string name, Ruble rubles) : this()
        {
            Name = name;
            AccountRuble.Add(rubles.Value);
        }

        #endregion

        #region Методы
        public void AddMoney(Ruble value)
        {
            AccountRuble += value;
            this.OnChangedAccountRubles?.Invoke(addSuccesAccountRubleString);
        }

        public void SpendMoney(Ruble value)
        {
            if (CanSpendMoney(value))
            {
                AccountRuble -= value;
                this.OnChangedAccountRubles?.Invoke(spendSuccesAccountRubleString);
            }
            else
            {
                this.OnChangedAccountRubles?.Invoke(spendFaildAccountRubleString);
            }
        }

        public bool CanSpendMoney(Ruble value)
        {
            return AccountRuble >= value;
        }

        private int GetID() => ++id;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
		#endregion

		#region
		public override string ToString()
		{
            return $"{Name}, {AccountRuble}";
		}
		#endregion
	}
}
