using BankAccount_Library.account;
using BankAccount_Library.deposit;
using BankAccount_Library.currency;

namespace Lesson10.transaction
{
    public class TransactionBank : ITransaction<BankA>
    {
        public TransactionBank() 
        {

        }
        public void TransferAccountsBetweenClients(ICapital<BankAccount> account1, ICapital<BankAccount> account2, Ruble rubles)
        {
            if (account1 == account2)
            {
                Post.PostMessage($"Выберете разные счета!");
                return;
            }
            
                if ((account1 as BankAccount).CanSpendMoney(rubles))
                {
                    (account1 as BankAccount).SpendMoney(rubles);
                    (account2 as BankAccount).AddMoney(rubles);
                    Post.PostMessage($"Перевод счетами между клиентами осуществлен");
                }
        }
    }
}
