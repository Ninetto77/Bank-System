using System.Collections.Generic;
using System.Collections.ObjectModel;
using Lesson10.observer;
using BankAccount_Library.account;


namespace Lesson10.activityLog
{
    public class ActivityJournal : IObserver
    {
        public ObservableCollection<ActivityLog> ActivityLogsList;

        public ActivityJournal()
        {
            ActivityLogsList = new ObservableCollection<ActivityLog>();
        }

        public void Update(Client client, TypeOfAct type, BankAccount account = null)
        {
            string nameClient = client.Name;
            string nameAccount;

            if (account == null)
                nameAccount = "-";
            else
                nameAccount = account.Name;

            List<string> logsName = new List<string>
            {
                nameClient,
                nameAccount
            };

            ActivityLog log = new ActivityLog(logsName, type);
            ActivityLogsList.Add(log);
        }
    }
}
