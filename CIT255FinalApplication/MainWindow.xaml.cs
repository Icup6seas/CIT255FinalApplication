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

        private void txt_ItemTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

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
                listManager.Additem(txt_ItemTextBox.Text.Trim());
            }
            txt_ItemTextBox.Text = "";
            lst_ItemsInList.Items.Refresh();
        }
    }
}
