using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace CIT255FinalApplication
{
    public class XmlDataService : IDataServices
    {
        private string _dataFilePath;

        public List<ListOfItem> ReadAll()
        {
            List<ListOfItem> listOfItems = new List<ListOfItem>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<ListOfItem>), new XmlRootAttribute("ListOfItems"));

            try
            {
                StreamReader reader = new StreamReader(_dataFilePath);
                using (reader)
                {
                    listOfItems = (List<ListOfItem>)serializer.Deserialize(reader);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return listOfItems;
        }

        public void WriteAll(List<ListOfItem> listOfItems)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ListOfItem>), new XmlRootAttribute("ListOfItems"));

            try
            {
                StreamWriter writer = new StreamWriter(_dataFilePath);
                using (writer)
                {
                    serializer.Serialize(writer, listOfItems);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public XmlDataService(string datafile)
        {
            _dataFilePath = datafile;
        }
    }
}
