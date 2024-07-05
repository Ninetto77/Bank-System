using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lesson10.activityLog
{
    public class ActivityLog: INotifyPropertyChanged
    {
        #region Поля
        private string clientSurname;
        private string accountName;
        private string actString;
        private string timeOfAction;
        #endregion

        #region Свойства
        public string ClientSurname
        {
            get { return clientSurname; }
            set
            {
                clientSurname = value;
                OnPropertyChanged(nameof(this.ClientSurname));
            }
        }
        public string AccountName
        {
            get { return accountName; }
            set
            {
                accountName = value;
                OnPropertyChanged(nameof(this.AccountName));
            }
        }
        public string ActString
        {
            get { return actString; }
            set
            {
                actString = value;
                OnPropertyChanged(nameof(this.ActString));
            }
        }
        public string TimeOfAction
        {
            get { return timeOfAction; }
            set
            {
                timeOfAction = value;
                OnPropertyChanged(nameof(this.TimeOfAction));
            }
        }
        #endregion

        #region Конструкторы
        public ActivityLog(List<string> logs, TypeOfAct type)
        {
            AddLog(logs, type);
        }
        #endregion

        #region Методы

        /// <summary>
        /// Добавляет запись в журнал
        /// </summary>
        /// <param name="logs"></param>
        /// <param name="type"></param>
        public void AddLog(List<string> logs, TypeOfAct type)
        {
            ClientSurname = logs[0];
            AccountName = logs[1];
            TimeOfAction = DateTime.Now.ToShortTimeString();
            act = type;

            TypeOfActToString();
        }

        private void TypeOfActToString()
        {
            switch (act)
            {
                case TypeOfAct.openAccount:
                    ActString = "Открытие счета";
                    break;
                case TypeOfAct.closeAccount:
                    ActString = "Закрытие счета";
                    break;
                case TypeOfAct.addMoneyAccount:
                    ActString = "Добавление средств";
                    break;
                case TypeOfAct.spendMoneyAccount:
                    ActString = "Извлечение средств";
                    break;
                case TypeOfAct.transferMoneyAccount:
                    ActString = "Перевод между счетами";
                    break;
                case TypeOfAct.transferBetweenClientsMoney:
                    ActString = "Перевод другому клиенту";
                    break;
                case TypeOfAct.changeClientData:
                    ActString = "Изменение данных об клиенте";
                    break;
                case TypeOfAct.addClient:
                    ActString = "Добавление клиента";
                    break;                
                case TypeOfAct.deleteClient:
                    ActString = "Удаление клиента";
                    break;

            }
        }

        #region Другое
        public TypeOfAct act;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        #endregion



    }

    public enum TypeOfAct
    {
        openAccount,
        closeAccount,
        addMoneyAccount,
        spendMoneyAccount,
        transferMoneyAccount,
        transferBetweenClientsMoney,
        changeClientData,
        addClient,
        deleteClient
    }
}
