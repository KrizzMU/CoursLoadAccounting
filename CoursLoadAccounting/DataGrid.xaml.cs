using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace CoursLoadAccounting
{
    /// <summary>
    /// Interaction logic for DataGrid.xaml
    /// </summary>
    public partial class DataGrid : Window
    {

        public PostgresDataBase databaseUniversity;
        public DataGrid(PostgresDataBase postgresDataBase)
        {
            databaseUniversity = postgresDataBase;
            InitializeComponent();
                
        }

       
        private void SelectTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            databaseUniversity.Open();

            TableDB.ItemsSource = databaseUniversity.GetTable(SelectTable.SelectedIndex).DefaultView;
         
            databaseUniversity.Close();
        }

        

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

            databaseUniversity.Open();

            TableDB.ItemsSource = databaseUniversity.SearchTable(SelectTable.SelectedIndex, Search.Text).DefaultView;

            databaseUniversity.Close();
        }

        private void AddButtom_Click(object sender, RoutedEventArgs e)
        {
            
            
            AddWindow AddWindow = new AddWindow(SelectTable.SelectedIndex, databaseUniversity);
            AddWindow.ShowDialog();
            AddWindow.Owner = this;

            databaseUniversity.Open();

            TableDB.ItemsSource = databaseUniversity.GetTable(SelectTable.SelectedIndex).DefaultView;

            databaseUniversity.Close();

        }

        private void DelButtom_Click(object sender, RoutedEventArgs e)
        {
            if (TableDB.SelectedIndex > -1)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить эту запись?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    databaseUniversity.Delete(SelectTable.SelectedIndex, TableDB.SelectedIndex);

                    databaseUniversity.Open();

                    TableDB.ItemsSource = databaseUniversity.GetTable(SelectTable.SelectedIndex).DefaultView;

                    databaseUniversity.Close();
                }

            }
            else
                MessageBox.Show("Выберете строку!");
        }

        private void EditButtom_Click(object sender, RoutedEventArgs e)
        {
            if (TableDB.SelectedIndex > -1)
            {

                AddWindow AddWindow = new AddWindow(SelectTable.SelectedIndex, databaseUniversity, TableDB.SelectedIndex);
                AddWindow.ShowDialog();
                AddWindow.Owner = this;

                databaseUniversity.Open();

                TableDB.ItemsSource = databaseUniversity.GetTable(SelectTable.SelectedIndex).DefaultView;

                databaseUniversity.Close();
            }
            else
                MessageBox.Show("Выберете строку!");
        }

        private void TasksButton_Click(object sender, RoutedEventArgs e)
        {
            Task task = new Task(databaseUniversity);
            
            task.Show();

            this.Close();
        }
    }
}
