using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIT255FinalApplication
{
    public interface IDataServices
    {
        List<ListOfItem> ReadAll();
        void WriteAll(List<ListOfItem> listOfItems);
    }
}
