using Lesson10.activityLog;
using BankAccount_Library.account;
using BankLogic_Library.DB;


namespace Lesson10.observer
{
    public interface IObserver
    {
        void Update(Clients client, TypeOfAct type, BankAccount account = null);
    }
}
