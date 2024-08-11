using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lesson10.utills
{
    internal class XMALWork
    {
        public static void SerializeFields<T>(ObservableCollection<T> consultate, string filePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<T>));

            Stream fstream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            xmlSerializer.Serialize(fstream, consultate);
            fstream.Close();
        }

        public static async Task<IEnumerable<T>> DeserializeField<T>(string filePath)
        {
            IEnumerable<T> temp = new ObservableCollection<T>();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<T>));

            Stream fstream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            temp = xmlSerializer.Deserialize(fstream) as IEnumerable<T>;

            fstream.Close();

			//Thread.Sleep(2000);


			return temp;
        }
    }
}
