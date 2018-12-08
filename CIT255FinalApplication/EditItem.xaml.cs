using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CIT255FinalApplication
{
    /// <summary>
    /// Interaction logic for EditItem.xaml
    /// </summary>
    public partial class EditItem : Window
    {

        private string temporaryDesc;
        public EditItem(ListItem item)
        {
            InitializeComponent();
            DataContext = item;
            
            temporaryDesc = item.ItemDescription;
        }

        private void btn_EditOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void EditBox_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool output = DialogResult ?? false;
            if (!output) (DataContext as ListItem).ItemDescription = temporaryDesc;
        }
    }
}
