using BankAccount_Library.account;

namespace BankAccount_Library.deposit
{
    public interface ICapital<out T> where T: BankAccount
    {
        string Name { get; }
        float AddPercentageForMonth(float money, int per);
        void AddPercentageForYear(byte per);
    }
}
