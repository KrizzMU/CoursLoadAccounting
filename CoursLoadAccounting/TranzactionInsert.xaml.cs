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

namespace CoursLoadAccounting
{
    /// <summary>
    /// Interaction logic for TranzactionInsert.xaml
    /// </summary>
    public partial class TranzactionInsert : Window
    {
        public PostgresDataBase databaseUniversity;
        public TranzactionInsert(PostgresDataBase databaseUniversity)
        {
            this.databaseUniversity = databaseUniversity;

            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            databaseUniversity.Open();

            databaseUniversity.Tranzaction(FirstName.Text.Trim(), SecondName.Text.Trim(), Phone.Text.Trim(), LastName.Text.Trim(), Email.Text.Trim(), Faculty.Text.Trim());

            databaseUniversity.Close();
        }
    }
}
