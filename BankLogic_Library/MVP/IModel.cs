using BankLogic_Library.DB;


namespace Lesson10.MVP
{
    public interface IModel
    {
        //все методы работы с клиентами и банком
        void ChangeClientData(Clients client, int index, Worker worker);
    }
}
