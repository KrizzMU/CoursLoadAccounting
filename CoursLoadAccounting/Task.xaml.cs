﻿using System;
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
    /// Interaction logic for Task.xaml
    /// </summary>
    public partial class Task : Window
    {
        public PostgresDataBase databaseUniversity;       

        public Task(PostgresDataBase postgresDataBase)
        {
            databaseUniversity = postgresDataBase;
            
            InitializeComponent();

            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = new DataGrid(databaseUniversity);
            dataGrid.Show();
            this.Close();
        }

        private void SelectTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            databaseUniversity.Open();

            TableDB.ItemsSource = databaseUniversity.GetTableForTask(SelectTable.SelectedIndex).DefaultView;

            databaseUniversity.Close();
        }
    }
}
