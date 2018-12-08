using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CIT255FinalApplication
{
    public class ListOfItem
    {
        [XmlElement("Id")]
        public int Id { get; set; }
    }
}
