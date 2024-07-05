using BankAccount_Library.account;
using BankAccount_Library.currency;

namespace BankAccount_Library.deposit
{
    public class AccountWithCapitalization : BankAccount, ICapital<BankAccount>
    {
        public AccountWithCapitalization() : base()
        {
        }
        public AccountWithCapitalization(Ruble ruble) : base(ruble)
        {
        }
        public AccountWithCapitalization(string name, Ruble ruble) : base(name, ruble)
        {
        }


        public float AddPercentageForMonth(float money, int per)
        {
            var temp = money;
            temp = (temp * per / 100);
            temp /= 12;

            var i = money += temp;

            return i;
        }

        public void AddPercentageForYear(byte per)
        {
            Ruble rubles = this.AccountRuble;
            float moneyForMonth = rubles.Value;
            int months = 12;

            for (int i = 0; i < months; i++)
            {
                moneyForMonth = AddPercentageForMonth(moneyForMonth, per);
            }

            AddMoney(new Ruble(moneyForMonth));
        }
    }
}
