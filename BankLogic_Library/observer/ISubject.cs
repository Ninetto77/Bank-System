using Lesson10.activityLog;
using BankAccount_Library.account;

namespace Lesson10.observer
{
    public interface ISubject
    {
        void Attach(IObserver observer);

        void Detach(IObserver observer);

        void Notify(Client client, TypeOfAct type, BankAccount account = null);
    }
}
