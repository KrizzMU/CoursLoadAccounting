using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace CoursLoadAccounting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PostgresDataBase databaseUniversity;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                logConnect_Click(sender, e);
            }
        }

        private void logConnect_Click(object sender, RoutedEventArgs e)
        {
            databaseUniversity = new PostgresDataBase(/*logLogin.Text, logPassword.Password*/ "postgres", "Alan123231");
            try
            {    
                databaseUniversity.Open();
                databaseUniversity.Close();
                DataGrid dataGrid = new DataGrid(databaseUniversity);                
                dataGrid.Show();
                
                this.Close();
            }
            catch
            {
                MessageBox.Show("Неверный логин или пароль!", "Login Error");
            }           
        }
    }
}
