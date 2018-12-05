using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Configuration;
using System.IO;
using System.ComponentModel;

namespace CIT255FinalApplication
{
    [Serializable]
    public class ListItem : INotifyPropertyChanged
    {

    }

    [Serializable]
    public class ListManager : INotifyPropertyChanged
    {
        [NonSerialized]
        private XDocument listManager;
        [NonSerialized]
        private bool doneItems;
        [NonSerialized]
        private List<ListItem> items;

        public List<ListItem> Items
        {
            get
            {
                if (doneItems)
                {
                    return items;
                }
                else
                {
                    return (from i in items
                            where i.DateTimeDone == ""
                            select i).ToList();
                }
            }
        }


    }
}
