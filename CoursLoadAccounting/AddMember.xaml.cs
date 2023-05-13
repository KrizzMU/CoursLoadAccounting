using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
//using System.Reflection.Emit;
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
    /// Interaction logic for AddMember.xaml
    /// </summary>
    public partial class AddMember : Window
    {
        public PostgresDataBase databaseUniversity;

        public List<Label> labels = new List<Label>();

        public List<TextBox> textBoxes = new List<TextBox>();

        public List<ComboBox> comboBoxes = new List<ComboBox>();

        private List<Dictionary<int, int>> getIdFromComboboxes = new List<Dictionary<int, int>>();

        public AddMember(int ind, PostgresDataBase databaseUniversity)
        {
            this.databaseUniversity = databaseUniversity;

            InitializeComponent();

            if (ind == 0) { }
            if (ind == 1) 
            {
                this.Title = "Добавление кафедры";
                this.Height = 355;

                labels.Add(GetLabel("Название кафедры:"));
                textBoxes.Add(GetTextBox());

                labels.Add(GetLabel("Завкафедрой:"));
                comboBoxes.Add(GetMembers());
                comboBoxes[0].SelectionChanged += OtherMembers_Click;                

                labels.Add(GetLabel("Факультет:"));
                comboBoxes.Add(GetFacultets());
                comboBoxes[1].SelectionChanged += OtherFaculty_Click;               

                Button AddKafedra = GetButton();
               
                AddKafedra.Click += AddKafedra_Click;

                addPanel.Children.Add(labels[0]);
                addPanel.Children.Add(textBoxes[0]);
                addPanel.Children.Add(labels[1]);
                addPanel.Children.Add(comboBoxes[0]);
                addPanel.Children.Add(labels[2]);
                addPanel.Children.Add(comboBoxes[1]);
                addPanel.Children.Add(AddKafedra);
            }
            if (ind == 2) 
            {
                this.Height = 290;
                this.Title = "Добавление факультета";

                labels.Add(GetLabel("Название факультета"));
                textBoxes.Add(GetTextBox());

                labels.Add(GetLabel("Выберетие декана:"));

                comboBoxes.Add(GetMembers());
                comboBoxes[0].SelectionChanged += OtherMembers_Click;
                
                Button AddFaculty = GetButton();
                AddFaculty.Click += AddFaculty_Click;

                addPanel.Children.Add(labels[0]);
                addPanel.Children.Add(textBoxes[0]);
                addPanel.Children.Add(labels[1]);
                addPanel.Children.Add(comboBoxes[0]);

                addPanel.Children.Add(AddFaculty);

            }
            if (ind == 3)
            {
                this.Title = "Добавление руководителя";
                this.Height = 450;

                labels.Add(GetLabel("Имя"));
                labels.Add(GetLabel("Фамилие"));
                labels.Add(GetLabel("Отчество"));
                labels.Add(GetLabel("Почта(если есть)"));
                labels.Add(GetLabel("Номер телефона"));

                textBoxes.Add(GetTextBox());
                textBoxes.Add(GetTextBox());
                textBoxes.Add(GetTextBox());
                textBoxes.Add(GetTextBox());
                textBoxes.Add(GetTextBox());
                
                addPanel.Children.Add(labels[0]);
                addPanel.Children.Add(textBoxes[0]);

                addPanel.Children.Add(labels[1]);
                addPanel.Children.Add(textBoxes[1]);

                addPanel.Children.Add(labels[2]);
                addPanel.Children.Add(textBoxes[2]);

                addPanel.Children.Add(labels[3]);
                addPanel.Children.Add(textBoxes[3]);

                addPanel.Children.Add(labels[4]);
                addPanel.Children.Add(textBoxes[4]);

                Button addMembers = GetButton();              
                addMembers.Click += AddMembers_Click;

                addPanel.Children.Add(addMembers);                
            }
            if (ind == 4) { }
            if (ind == 5) { }           
        }
        private ComboBox GetMembers()
        {
            ComboBox comboBox = GetComboBox();

            databaseUniversity.Open();

            var readerMembers = databaseUniversity.ExecuteQuery("SELECT id, CONCAT(firstname, ' ', middlename, ', ', phonenumber) FROM departmentmembers");

            if (readerMembers.HasRows)
            {
                int i = 0;

                Dictionary<int, int> getId = new Dictionary<int, int>();

                while (readerMembers.Read())
                {
                    getId.Add(i, (int)readerMembers.GetValue(0));
                    comboBox.Items.Add((string)readerMembers.GetValue(1));
                    i++;
                }

                getIdFromComboboxes.Add(getId);
            }

            databaseUniversity.Close();

            comboBox.Items.Add("Другой");
            comboBox.SelectedIndex = 0;

            return comboBox;
        }

        private ComboBox GetFacultets()
        {
            ComboBox comboBox = GetComboBox();

            databaseUniversity.Open();

            var reader = databaseUniversity.ExecuteQuery("SELECT id, name FROM faculty");

            if (reader.HasRows)
            {
                int i = 0;

                Dictionary<int, int> getId = new Dictionary<int, int>();

                while (reader.Read())
                {
                    getId.Add(i, (int)reader.GetValue(0));
                    comboBox.Items.Add((string)reader.GetValue(1));
                    i++;
                }

                getIdFromComboboxes.Add(getId);
            }

            databaseUniversity.Close();

            comboBox.Items.Add("Другой");           
            comboBox.SelectedIndex = 0;

            return comboBox;
        }
        
        private void OtherMembers_Click(object sender, SelectionChangedEventArgs e)
        {
            int n = comboBoxes[0].Items.Count - 1;
            if (comboBoxes[0].SelectedIndex == n)
            {
                AddMember addMember = new AddMember(3, databaseUniversity);
                addMember.ShowDialog();

                databaseUniversity.Open();

                int count = (int)(long)databaseUniversity.ExecuteScalar("SELECT COUNT(*) FROM departmentmembers");
                if (count > n)
                {
                    
                    var readerMembers = databaseUniversity.ExecuteQuery("SELECT id, CONCAT(firstname, ' ', middlename, ', ', phonenumber) as dek FROM departmentmembers " +
                                                                        "ORDER BY id DESC LIMIT 1");
                    if (readerMembers.HasRows)
                    {
                        comboBoxes[0].Items.RemoveAt(n);

                        readerMembers.Read();

                        comboBoxes[0].Items.Add((string)readerMembers["dek"]);

                        getIdFromComboboxes[0].Add(n, (int)readerMembers["id"]);

                        comboBoxes[0].Items.Add("Другой");
                        comboBoxes[0].SelectedIndex = comboBoxes[0].Items.Count - 2;
                    }                    
                }
                else
                    comboBoxes[0].SelectedIndex = 0;

                databaseUniversity.Close();
            }          
        }

        private void OtherFaculty_Click(object sender, SelectionChangedEventArgs e)
        {
            int n = comboBoxes[1].Items.Count - 1;
            if (comboBoxes[1].SelectedIndex == n)
            {
                AddMember addMember = new AddMember(2, databaseUniversity);
                addMember.ShowDialog();

                databaseUniversity.Open();

                int count = (int)(long)databaseUniversity.ExecuteScalar("SELECT COUNT(*) FROM faculty");
                if (count > n)
                {                    
                    var reader = databaseUniversity.ExecuteQuery("SELECT id, name FROM faculty " +
                                                                        "ORDER BY id DESC LIMIT 1");
                    if (reader.HasRows)
                    {
                        comboBoxes[1].Items.RemoveAt(n);

                        reader.Read();

                        comboBoxes[1].Items.Add((string)reader["name"]);

                        getIdFromComboboxes[1].Add(n, (int)reader["id"]);

                        comboBoxes[1].Items.Add("Другой");
                        comboBoxes[1].SelectedIndex = comboBoxes[1].Items.Count - 2;
                    }                                        
                }
                else
                    comboBoxes[1].SelectedIndex = 0;

                databaseUniversity.Close();
            }
            
        }

        private void AddFaculty_Click(object sender, RoutedEventArgs e) // Сделать проверку на допустимость символов
        {
            
            databaseUniversity.Open();
            
            databaseUniversity.ExecuteNonQuery($"CALL add_faculty('{textBoxes[0].Text}', {getIdFromComboboxes[0][comboBoxes[0].SelectedIndex]});");

            databaseUniversity.Close();

            this.Close();
        }

        private void AddMembers_Click(object sender, RoutedEventArgs e) // Сделать проверку на допустимость значений.
        {
            databaseUniversity.Open();
            databaseUniversity.ExecuteNonQuery($"CALL add_departmentmember('{textBoxes[0].Text}', '{textBoxes[1].Text}', '{textBoxes[4].Text}', " +
                                                                         $"'{textBoxes[2].Text}', '{textBoxes[3].Text}');");
            databaseUniversity.Close();

            this.Close();
        }

        private void AddKafedra_Click(object sender, RoutedEventArgs e)
        {         
            databaseUniversity.Open();
            databaseUniversity.ExecuteNonQuery($"CALL add_kafedra('{textBoxes[0].Text}', " +
                                               $"{getIdFromComboboxes[0][comboBoxes[0].SelectedIndex]}, {getIdFromComboboxes[1][comboBoxes[1].SelectedIndex]});");
            databaseUniversity.Close();

            this.Close();
        }

        private Label GetLabel(string name)
        {
            Label label = new Label();

            label.Content = name;

            label.FontSize = 18;

            label.HorizontalAlignment = HorizontalAlignment.Center;

            return label;
        }

        private TextBox GetTextBox()
        {
            TextBox textBox = new TextBox();

            textBox.HorizontalAlignment = HorizontalAlignment.Center;

            textBox.FontSize = 18;

            textBox.Width = 300;

            textBox.Margin = new Thickness(0, 0, 0, 10);

            return textBox;
        }

        private Button GetButton()
        {
            Button button = new Button();

            button.Content = "Добавить";

            button.Margin = new Thickness(10, 0, 10, 10);

            button.Width = 300;
            button.Height = 50;

            return button;
        }

        private ComboBox GetComboBox()
        {
            ComboBox comboBox = new ComboBox();

            comboBox.HorizontalAlignment = HorizontalAlignment.Center;

            comboBox.FontSize = 14;

            comboBox.Width = 300;

            comboBox.Margin = new Thickness(0, 0, 0, 30);            

            return comboBox;
        }
    }
}
