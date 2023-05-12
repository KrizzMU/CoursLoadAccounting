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
            
            
            AddMember addMember = new AddMember(SelectTable.SelectedIndex, databaseUniversity);
            addMember.ShowDialog();
            addMember.Owner = this;

            databaseUniversity.Open();

            TableDB.ItemsSource = databaseUniversity.GetTable(SelectTable.SelectedIndex).DefaultView;

            databaseUniversity.Close();

        }
    }
}
