using System;

namespace BankAccount_Library.currency
{
    public class Ruble: Currency<float>
    {
        public override event Action<float> OnValueChangedEvent;
        
        #region Конструкторы
        public Ruble() 
        {
            this.Value = 0f;
        }

        public Ruble(float value)
        {
            this.Value = value;
        }
        #endregion

        #region Методы
        public override void Add(float amount)
        {
            this.Value = this.Value + amount;
            OnValueChangedEvent?.Invoke(this.Value);
        }

        public override void Spend(float amount)
        {
            if (this.Value >= amount)
            {
                this.Value = this.Value - amount;
                OnValueChangedEvent?.Invoke(this.Value);
            }
            else
                return;
        }
        #endregion

        #region Перегрузки
        public static Ruble operator +(Ruble currency1, Ruble currency2)
        {
            return new Ruble(currency1.Value + currency2.Value);
        }

        public static Ruble operator -(Ruble currency1, Ruble currency2)
        {
            return new Ruble(currency1.Value - currency2.Value);
        }

        public static bool operator >=(Ruble currency1, Ruble currency2)
        {
            if (currency1.Value >= currency2.Value)
                return true;
            return false;
        }

        public static bool operator <=(Ruble currency1, Ruble currency2)
        {
            if (currency1.Value <= currency2.Value)
                return true;
            return false;
        }
        #endregion
    }



}
