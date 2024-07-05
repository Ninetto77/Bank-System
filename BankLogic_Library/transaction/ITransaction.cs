using BankAccount_Library.account;
using BankAccount_Library.deposit;
using BankAccount_Library.currency;

namespace Lesson10.transaction
{
    public interface ITransaction<in T> where T: BankA
    {
        void TransferAccountsBetweenClients(ICapital<BankAccount> account1, ICapital<BankAccount> account2, Ruble rubles);
    }
}
