using System;

namespace BankAccount_Library.currency
{
    public class Dollar: Currency<int>
    {

        public override event Action<int> OnValueChangedEvent;

        #region Конструкторы
        public Dollar()
        {
            this.Value = 0;
        }

        public Dollar(int value)
        {
            this.Value = value;
        }
        #endregion

        #region Методы
        public override void Add(int amount)
        {
            this.Value = this.Value + amount;
            OnValueChangedEvent(this.Value);
        }

        public override void Spend(int amount)
        {
            if (this.Value >= amount)
            {
                this.Value = this.Value - amount;
                OnValueChangedEvent(this.Value);
            }
            else
                return;
        }
        #endregion

        #region Перегрузки
        public static Dollar operator +(Dollar currency1, Dollar currency2)
        {
            return new Dollar(currency1.Value + currency2.Value);
        }

        public static Dollar operator -(Dollar currency1, Dollar currency2)
        {
            return new Dollar(currency1.Value - currency2.Value);
        }
        public static bool operator >=(Dollar currency1, Dollar currency2)
        {
            if (currency1.Value >= currency2.Value)
                return true;
            return false;
        }

        public static bool operator <=(Dollar currency1, Dollar currency2)
        {
            if (currency1.Value <= currency2.Value)
                return true;
            return false;
        }
        #endregion
    }
}
