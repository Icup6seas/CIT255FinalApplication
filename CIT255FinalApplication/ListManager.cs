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
        private XElement listmanager;
        internal XElement ListManager { get { return listmanager; } }
        internal ListItem(XElement listmanager)
        {
            this.listmanager = listmanager;
        }
        public string DateCreated
        {
            get { return listmanager.Element("datecreated").Value; }
            set
            {
                listmanager.Element("datecreated").Value = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("DateCreated"));
                }
            }
        }
        public string ItemDescription
        {
            get { return listmanager.Element("itemdescription").Value; }
            set
            {
                listmanager.Element("itemdescription").Value = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ItemDescription"));
                }
            }
        }
        public string DateComplete
        {
            get { return listmanager.Element("datecomplete") == null ? "" : 
                    listmanager.Element("datecomplete").Value; }
            set
            {
                if (listmanager.Element("datecomplete") == null)
                {
                    listmanager.Add(new XElement("datecomplete"));
                }

                listmanager.Element("datecomplete").Value = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("DateComplete"));
                }
            }
        }
        public bool IsComplete { get { return (listmanager.Element("datecomplete") != null); } }

        public override string ToString()
        {
            string output;
            if (DateComplete != "")
            {
                DateTime dateDone = DateTime.Parse(DateComplete);
                output = String.Format("{0} - Completed: {1:MM-dd-yyyy}", ItemDescription, dateDone);
            }
            else
            {
                output = ItemDescription;
            }
            return output;
        }

        public event PropertyChangedEventHandler PropertyChanged;
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
                            where i.DateComplete == ""
                            select i).ToList();
                }
            }
        }
        public bool DoneItems
        {
            get { return doneItems; }
            set
            {
                doneItems = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("DoneItems"));
                    PropertyChanged(this, new PropertyChangedEventArgs("Items"));
                }
            }
        }
        public ListManager()
        {
            AppSettingsReader asr = new AppSettingsReader();
            string listFile = (string)asr.GetValue("listFile", typeof(string));
            if (File.Exists(listFile))
            {
                listManager = XDocument.Load(listFile);
                items = (from e in listManager.Root.Elements() select new ListItem(e)).ToList();
            }
            else
            {
                listManager = new XDocument();
                listManager.Add(new XElement("listManagerItems"));
                items = new List<ListItem>();
            }
            
        }
        public void AddItem(string itemDescription)
        {
            XElement item = new XElement("listitem");
            XElement dateMade = new XElement("dateMade");
            dateMade.Value = DateTime.Now.ToString("MM-dd-yyyy");
            item.Add(dateMade);
            XElement itemDesc = new XElement("itemdescription");
            itemDesc.Value = itemDescription;
            item.Add(itemDesc);

            listManager.Root.Add(item);
            items.Add(new ListItem(item));

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Items"));
            }
        }
        public void DeleteItem(ListItem item)
        {
            items.Remove(item);
            item.ListManager.Remove();
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Items"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void Save()
        {
            AppSettingsReader asr = new AppSettingsReader();
            string listFile = (string)asr.GetValue("listFile", typeof(string));
            listManager.Save(listFile);
        }
    }
}
