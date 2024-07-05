using Lesson10.activityLog;
using BankAccount_Library.account;

namespace Lesson10.observer
{
    public interface IObserver
    {
        void Update(Client client, TypeOfAct type, BankAccount account = null);
    }
}
