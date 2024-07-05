using BankAccount_Library.account;
using BankAccount_Library.deposit;
using BankAccount_Library.currency;

namespace Lesson10.MVP
{
    public interface IModel
    {
        //все методы работы с клиентами и банком
        void ChangeClientData(Client client, int index, Worker worker);
    }
}
