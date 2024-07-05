using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BankAccount_Library.currency
{
    public abstract class Currency<T>: INotifyPropertyChanged
    {
        public abstract event Action<T> OnValueChangedEvent;
        public event PropertyChangedEventHandler PropertyChanged;

        private T value;
        public T Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                OnPropertyChanged("Value");
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public abstract void Add(T amount);
        public abstract void Spend(T amount);
    }
}
