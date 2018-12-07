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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CIT255FinalApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ListManager listManager = new ListManager();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = listManager;
            TextBoxClearAndFocus();
        }

        //
        //Clears the TextBox and gains focus
        private void TextBoxClearAndFocus()
        {
            if (txt_ItemTextBox.Text.Trim() != "" || txt_ItemTextBox.Text != null)
            {
                txt_ItemTextBox.Text = "";
            }

            txt_ItemTextBox.Focus();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_Help_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow help = new HelpWindow();
            help.Show();
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            if (!txt_ItemTextBox.Text.Trim().Equals(string.Empty))
            {
                listManager.AddItem(txt_ItemTextBox.Text.Trim());
            }
            txt_ItemTextBox.Text = "";
            lst_ItemsInList.Items.Refresh();
        }

        private void ClosingWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listManager.Save();
        }

        private void CheckDone(object sender, RoutedEventArgs e)
        {
            if (lst_ItemsInList.SelectedItem != null)
            {
                MessageBoxResult completed = MessageBox.Show("Complete", "Remove item?", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation, MessageBoxResult.No);
                if (completed == MessageBoxResult.Yes)
                {
                    listManager.DeleteItem(lst_ItemsInList.SelectedItem as ListItem);
                }
                lst_ItemsInList.Items.Refresh();
            }
        }

        private void EditMenu_Click(object sender, RoutedEventArgs e)
        {
            if (lst_ItemsInList.SelectedItem != null)
            {
                EditItem popUp = new EditItem(lst_ItemsInList.SelectedItem as ListItem);
                popUp.Owner = this;
                popUp.ShowDialog();
            }
        }

        private void chk_CompletedItems_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void lst_ItemsInList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }

}
