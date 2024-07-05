using System;
using System.Collections.Generic;

namespace Lesson10
{
    public class Manager : Worker
    {
        private int maxNameLength = 30;

        public override void ChangeClientSurname(string name, string changedSurname)
        {
            try
            {
                if (changedSurname.Length > maxNameLength)
                    throw new Exception($"Поле должно быть не более {maxNameLength} символов!\n У вас {changedSurname.Length}");

                foreach (var c in changedSurname)
                {
                    if (char.IsNumber(c))
                        throw new Exception("Неккоректный ввод данных");
                }

                foreach (Client client in clients)
                {
                    if (client.Name == name)
                    {
                        client.Surname = changedSurname;
                        client.InvokeOnChange();
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Post.PostErrorMessage(e.Message);
            }
            
        }
        public override void ChangeClientName(string name, string changedName)
        {
            try
            {
                if (changedName.Length > maxNameLength)
                    throw new Exception($"Поле должно быть не более {maxNameLength} символов!\n У вас {changedName.Length}");

                foreach (var c in changedName)
                {
                    if (char.IsNumber(c))
                        throw new Exception("Неккоректный ввод данных");
                }

                foreach (Client client in clients)
                {
                    if (client.Name == name)
                    {
                        client.Name = changedName;
                        client.InvokeOnChange();
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Post.PostErrorMessage(e.Message);
            }
        }
        public override void ChangeClientMiddlename(string name, string changedMiddlename)
        {
            try
            {
                if (changedMiddlename.Length > maxNameLength)
                    throw new Exception($"Поле должно быть не более {maxNameLength} символов!\n У вас {changedMiddlename.Length}");

                foreach (var c in changedMiddlename)
                {
                    if (char.IsNumber(c))
                        throw new Exception("Неккоректный ввод данных");
                }

                foreach (Client client in clients)
                {
                    if (client.Name == name)
                    {
                        client.Middlename = changedMiddlename;
                        client.InvokeOnChange();
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Post.PostErrorMessage(e.Message);
            }
        }

        public override void ChangePhone(string name, string phone)
        {
            try
            {
                if (phone.Length != 11)
                {
                    throw new Exception($"Телефон должен содержать 11 цифр!\n У вас {phone.Length}");
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
                Post.PostErrorMessage(e.Message);
            }
        }
        public override void ChangePasportData(string name, string changedData)
        {
            try
            {
                if (changedData.Length != 10)
                {
                    throw new Exception($"Данные паспорта должны содержать 10 цифр!\n У вас {changedData.Length}");
                }
                foreach (var c in changedData)
                {
                    if (!char.IsNumber(c))
                        throw new Exception("Неккоректный ввод данных");
                }

                foreach (Client client in clients)
                {
                    if (client.Name == name)
                    {
                        client.PasportData = changedData;
                        client.InvokeOnChange();
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Post.PostErrorMessage(e.Message);
            }
        }
        public override void SortClientsBySurname()
        {
            base.SortClientsBySurname();
        }
        public override void AddClient(string id, string surname, string name,
                              string middlename, string phone,
                              string pasportData)
        {
            if (IsCorrectAllData(id, surname, name, middlename, phone, pasportData ))
                clients.Add(new Client(
                    $"{id}",
                    $"{surname}",
                    $"{name}",
                    $"{middlename}",
                    $"{phone}",
                    $"{pasportData}"
                    ));
        }

        /// <summary>
        /// Проверяет на корректность все поля
        /// </summary>
        /// <param name="id"></param>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="middlename"></param>
        /// <param name="phone"></param>
        /// <param name="pasportData"></param>
        /// <returns>Корректны ли все поля</returns>
        private bool IsCorrectAllData(string id, string surname, string name,
                              string middlename, string phone,
                              string pasportData)
        {
            List<string> s = new List<string>() { surname, name, middlename };
            foreach (var data in s)
                if (!IsCorrectNameData(data))
                    return false;

            List<string> n = new List<string>() { phone, pasportData };
            for (int i=11; i<13; i++)
                if (!IsCorrectNumericData(n[i - 11], i))
                    return false;

            return true;
        }

        /// <summary>
        /// проверяет на корректность поля с цифрами
        /// </summary>
        /// <param name="data"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        private bool IsCorrectNumericData(string data, int maxLength)
        {
            if (data.Length > maxLength)
            {  
                Post.PostErrorMessage($"Поле должно содержать {maxLength} цифр!\n У вас {data.Length}");
                return false; 
            }

            foreach (var c in data)
            {
                if (!char.IsNumber(c))
                {
                    Post.PostErrorMessage($"Неккоректный ввод данных");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// проверяет на корректность поля с буквами
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool IsCorrectNameData(string data)
        {
            if (data.Length > maxNameLength)
            {
                Post.PostErrorMessage($"Поле должно содержать не более {maxNameLength} цифр!\n У вас {data.Length}");
                return false;
            }

            foreach (var c in data)
            {
                if (char.IsNumber(c))
                {
                    Post.PostErrorMessage($"Неккоректный ввод данных");
                    return false;
                }
            }
            return true;
        }

    }
}
