﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using BankAccount_Library.account;
using BankAccount_Library.deposit;

namespace Lesson10.utills
{
    internal class XMALWork
    {
        public static void SerializeFields<T>(IEnumerable<T> consultate, string filePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(IEnumerable<T>));

            Stream fstream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            xmlSerializer.Serialize(fstream, consultate);
            fstream.Close();
        }

        public static IEnumerable<T> DeserializeField<T>(string filePath)
        {
            IEnumerable<T> temp = new ObservableCollection<T>();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<T>));

            Stream fstream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            temp = xmlSerializer.Deserialize(fstream) as IEnumerable<T>;

            fstream.Close();
            return temp;
        }
    }
}
