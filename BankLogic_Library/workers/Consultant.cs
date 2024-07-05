using System;
using System.Windows;

namespace Lesson10
{
    public class Consultate : Worker
    {

        public override void ChangeClientSurname(string surname, string changedSurname)
        {
            base.ChangeClientSurname(surname, changedSurname);
        }
        public override void ChangeClientName(string name, string changedName)
        {
            base.ChangeClientName(name, changedName);

        }
        public override void ChangeClientMiddlename(string Middlename, string changedMiddlename)
        {
            base.ChangeClientMiddlename(Middlename, changedMiddlename);
        }
        public override void ChangePhone(string name, string phone)
        {
            try
            {
                if (phone.Length != 11)
                {
                    throw new Exception();
                }

                foreach (var c in phone)
                {
                    if (!char.IsNumber(c))
                        throw new Exception("Неккоректный ввод данных");
                }

                foreach (Client client in clients)
                {
                    if (client.Name == name)
                    {
                        client.Phone = phone;
                        client.InvokeOnChange();
                        break;
                    }
                }

            }
            catch (Exception e)
            {
                Post.PostErrorMessage($"Телефон должен содержать 11 цифр!\n У вас {phone.Length}");
            }
        }
        public override void ChangePasportData(string name, string changedData)
        {
            base.ChangePasportData(name, changedData);
        }

        public override void SortClientsBySurname()
        {
            base.SortClientsBySurname();
        }

        public override void AddClient(string id, string surname, string name,
                      string middlename, string phone,
                      string pasportData)
        {
            base.AddClient(id, surname, name, middlename, phone, pasportData);
        }

    }
}

