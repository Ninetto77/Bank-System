using BankAccount_Library.account;
using BankAccount_Library.currency;

namespace BankAccount_Library.deposit
{
    public class AccountWithoutCapitalization : BankAccount, ICapital<BankAccount>
    {
        public AccountWithoutCapitalization() : base()
        {
        }
        public AccountWithoutCapitalization(Ruble ruble) : base(ruble)
        {
        }

        public AccountWithoutCapitalization(string name, Ruble ruble) : base(name, ruble)
        {
        }

        public float AddPercentageForMonth(float money, int per)
        {
            return money;
        }

        public void AddPercentageForYear(byte per)
        {
            Ruble rubles = this.AccountRuble;
            float moneyForMonth = rubles.Value;
            moneyForMonth = moneyForMonth + (moneyForMonth* per/100);

            AddMoney( new Ruble(moneyForMonth));
        }
    }
}
