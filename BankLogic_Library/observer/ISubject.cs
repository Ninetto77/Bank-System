using Lesson10.activityLog;
using BankAccount_Library.account;
using BankLogic_Library.DB;


namespace Lesson10.observer
{
    public interface ISubject
    {
        void Attach(IObserver observer);

        void Detach(IObserver observer);

        void Notify(Clients client, TypeOfAct type, BankAccount account = null);
    }
}
