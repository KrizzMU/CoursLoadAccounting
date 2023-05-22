using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
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
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public PostgresDataBase databaseUniversity;

        public List<Label> labels = new List<Label>();

        public List<TextBox> textBoxes = new List<TextBox>();

        public Dictionary<string, ComboBox> comboBoxes = new Dictionary<string, ComboBox>();
        
        private Dictionary<string, Dictionary<int, int>> getIdTable = new Dictionary<string, Dictionary<int, int>>();

        private int idStr = -1;
        
        public AddWindow(int ind, PostgresDataBase databaseUniversity)  
        {
            this.databaseUniversity = databaseUniversity;

            InitializeComponent();

            GetUI(ind, "Добавление");
                   
        }

        public AddWindow(int ind, PostgresDataBase databaseUniversity, int idStr) 
        {
            this.databaseUniversity = databaseUniversity;

            InitializeComponent();

            databaseUniversity.Open();

            this.idStr = databaseUniversity.GetId(ind, idStr);

            databaseUniversity.Close();

            GetUI(ind, "Изменение");
        }

        private void GetUI(int ind, string whatDo)
        {
            if (ind == 0)
            {
                this.Title = $"{whatDo} учета";
                this.Height = 660;

                labels.Add(GetLabel("Дисциплина:"));
                comboBoxes.Add("Discip", GetDisciplin());
                comboBoxes["Discip"].SelectionChanged += OtherDiscip_Сlick;

                labels.Add(GetLabel("Специальность:"));
                comboBoxes.Add("Special", GetSpecial());
                comboBoxes["Special"].SelectionChanged += OtherSpecial_Сlick;

                labels.Add(GetLabel("Лекции/час:"));
                textBoxes.Add(GetTextBox());

                labels.Add(GetLabel("Практики/час:"));
                textBoxes.Add(GetTextBox());

                labels.Add(GetLabel("Количество лаб:"));
                textBoxes.Add(GetTextBox());

                labels.Add(GetLabel("Семестр:"));
                textBoxes.Add(GetTextBox());

                labels.Add(GetLabel("Сессия:"));
                comboBoxes.Add("Sessia", GetComboBox());
                comboBoxes["Sessia"].Items.Add("Зачет");
                comboBoxes["Sessia"].Items.Add("Экзамен");
                comboBoxes["Sessia"].SelectedIndex = 0;

                addPanel.Children.Add(labels[0]);
                addPanel.Children.Add(comboBoxes["Discip"]);
                addPanel.Children.Add(labels[1]);
                addPanel.Children.Add(comboBoxes["Special"]);
                addPanel.Children.Add(labels[2]);
                addPanel.Children.Add(textBoxes[0]);
                addPanel.Children.Add(labels[3]);
                addPanel.Children.Add(textBoxes[1]);
                addPanel.Children.Add(labels[4]);
                addPanel.Children.Add(textBoxes[2]);
                addPanel.Children.Add(labels[5]);
                addPanel.Children.Add(textBoxes[3]);
                addPanel.Children.Add(labels[6]);
                addPanel.Children.Add(comboBoxes["Sessia"]);
                
                Button AddUchet = GetButton();

                AddUchet.Click += Uchet_Click;

                addPanel.Children.Add(AddUchet);

            }
            if (ind == 1)
            {
                this.Title = $"{whatDo} кафедры";
                this.Height = 355;

                labels.Add(GetLabel("Название кафедры:"));
                textBoxes.Add(GetTextBox());

                labels.Add(GetLabel("Завкафедрой:"));
                comboBoxes.Add("Members", GetMembers());
                comboBoxes["Members"].SelectionChanged += OtherMembers_Click;

                labels.Add(GetLabel("Факультет:"));
                comboBoxes.Add("Faculty", GetFacultets());
                comboBoxes["Faculty"].SelectionChanged += OtherFaculty_Click;

                Button AddKafedra = GetButton();

                AddKafedra.Click += Kafedra_Click;

                addPanel.Children.Add(labels[0]);
                addPanel.Children.Add(textBoxes[0]);
                addPanel.Children.Add(labels[1]);
                addPanel.Children.Add(comboBoxes["Members"]);
                addPanel.Children.Add(labels[2]);
                addPanel.Children.Add(comboBoxes["Faculty"]);
                addPanel.Children.Add(AddKafedra);
            }
            if (ind == 2)
            {
                this.Height = 290;
                this.Title = $"{whatDo} факультета";

                labels.Add(GetLabel("Название факультета"));
                textBoxes.Add(GetTextBox());

                labels.Add(GetLabel("Выберетие декана:"));

                comboBoxes.Add("Members", GetMembers());
                comboBoxes["Members"].SelectionChanged += OtherMembers_Click;

                Button AddFaculty = GetButton();
                AddFaculty.Click += Faculty_Click;

                addPanel.Children.Add(labels[0]);
                addPanel.Children.Add(textBoxes[0]);
                addPanel.Children.Add(labels[1]);
                addPanel.Children.Add(comboBoxes["Members"]);

                addPanel.Children.Add(AddFaculty);

            }
            if (ind == 3)
            {
                this.Title = $"{whatDo} руководителя";
                this.Height = 450;

                labels.Add(GetLabel("Имя"));
                labels.Add(GetLabel("Фамилия"));
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

                Button AddMember = GetButton();
                AddMember.Click += Member_Click;

                addPanel.Children.Add(AddMember);
            }
            if (ind == 4)
            {
                this.Title = $"{whatDo} дисциплины";
                this.Height = 290;

                labels.Add(GetLabel("Название дисциплины:"));
                textBoxes.Add(GetTextBox());

                labels.Add(GetLabel("Кафедра:"));
                comboBoxes.Add("Kafedr", GetKafedr());
                comboBoxes["Kafedr"].SelectionChanged += OtherKafedr_Click;

                Button AddDiscip = GetButton();
                AddDiscip.Click += Discip_Click;

                addPanel.Children.Add(labels[0]);
                addPanel.Children.Add(textBoxes[0]);
                addPanel.Children.Add(labels[1]);
                addPanel.Children.Add(comboBoxes["Kafedr"]);
                addPanel.Children.Add(AddDiscip);
            }
            if (ind == 5)
            {
                this.Title = $"{whatDo} специальности";
                this.Height = 355;

                labels.Add(GetLabel("Название специальности:"));
                textBoxes.Add(GetTextBox());

                labels.Add(GetLabel("Код специальности:"));
                textBoxes.Add(GetTextBox());

                labels.Add(GetLabel("Факультет:"));
                comboBoxes.Add("Faculty", GetFacultets());
                comboBoxes["Faculty"].SelectionChanged += OtherFaculty_Click;

                Button AddSpecial = GetButton();

                AddSpecial.Click += Special_Click;

                addPanel.Children.Add(labels[0]);
                addPanel.Children.Add(textBoxes[0]);
                addPanel.Children.Add(labels[1]);
                addPanel.Children.Add(textBoxes[1]);
                addPanel.Children.Add(labels[2]);
                addPanel.Children.Add(comboBoxes["Faculty"]);

                addPanel.Children.Add(AddSpecial);
            }
        }

        private ComboBox GetKafedr()
        {
            return GetFinishedBox("SELECT id, name FROM Kafedra", "Kafedr");
        }

        private ComboBox GetMembers()
        {
            return GetFinishedBox("SELECT id, CONCAT(firstname, ' ', middlename, ', ', phonenumber) FROM departmentmembers", "Members");         
        }

        private ComboBox GetFacultets()
        {
            return GetFinishedBox("SELECT id, name FROM faculty", "Faculty");
        }

        private ComboBox GetDisciplin()
        {
            return GetFinishedBox("SELECT id, name FROM Discipline", "Discip");
        }

        private ComboBox GetSpecial()
        {
            return GetFinishedBox("SELECT id, name FROM Speciality", "Special");            
        }

        private ComboBox GetFinishedBox(string cmd, string getIdKey)
        {
            ComboBox comboBox = GetComboBox();

            databaseUniversity.Open();

            var reader = databaseUniversity.ExecuteQuery(cmd);

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

                getIdTable.Add(getIdKey, getId);
            }

            databaseUniversity.Close();

            comboBox.Items.Add("Другой");
            comboBox.SelectedIndex = 0;

            return comboBox;
        }

        private void OtherMembers_Click(object sender, SelectionChangedEventArgs e)
        {
            OtherClick("departmentmembers", "Members", "SELECT id, CONCAT(firstname, ' ', middlename, ', ', phonenumber) as name " +
                "FROM departmentmembers ORDER BY id DESC LIMIT 1", 3);            
        }

        private void OtherFaculty_Click(object sender, SelectionChangedEventArgs e)
        {
            OtherClick("faculty", "Faculty", "SELECT id, name FROM faculty ORDER BY id DESC LIMIT 1", 2);           
        }

        private void OtherKafedr_Click(object sender, SelectionChangedEventArgs e)
        {
            OtherClick("Kafedra", "Kafedr", "SELECT id, name FROM Kafedra ORDER BY id DESC LIMIT 1", 1);
        }

        private void OtherDiscip_Сlick(object sender, SelectionChangedEventArgs e)
        {
            OtherClick("Discipline", "Discip", "SELECT id, name FROM Discipline ORDER BY id DESC LIMIT 1", 4);         
        }

        private void OtherSpecial_Сlick(object sender, SelectionChangedEventArgs e)
        {
            OtherClick("Speciality", "Special", "SELECT id, name FROM Speciality ORDER BY id DESC LIMIT 1", 5);            
        }

        private void OtherClick(string table, string getIdKey, string cmd, int nextWindow)
        {
            int n = comboBoxes[getIdKey].Items.Count - 1;
            if (comboBoxes[getIdKey].SelectedIndex == n)
            {
                AddWindow AddWindow = new AddWindow(nextWindow, databaseUniversity);
                AddWindow.ShowDialog();

                databaseUniversity.Open();

                int count = (int)(long)databaseUniversity.ExecuteScalar("SELECT COUNT(*) FROM " + table);
                if (count > n)
                {

                    var readerMembers = databaseUniversity.ExecuteQuery(cmd);
                    if (readerMembers.HasRows)
                    {
                        comboBoxes[getIdKey].Items.RemoveAt(n);

                        readerMembers.Read();

                        comboBoxes[getIdKey].Items.Add((string)readerMembers["name"]);

                        getIdTable[getIdKey].Add(n, (int)readerMembers["id"]);

                        comboBoxes[getIdKey].Items.Add("Другой");
                        comboBoxes[getIdKey].SelectedIndex = comboBoxes[getIdKey].Items.Count - 2;
                    }
                }
                else
                    comboBoxes[getIdKey].SelectedIndex = 0;

                databaseUniversity.Close();
            }
        }

        private void Faculty_Click(object sender, RoutedEventArgs e)
        {           
            databaseUniversity.Open();

            if ((bool)databaseUniversity.ExecuteScalar($"SELECT CheckFaculty('{textBoxes[0].Text.Trim()}')")) 
            {
                MessageBox.Show("Данный факультет уже существует!");
            }
            else
            {
                try
                {
                    if(idStr == -1)
                        databaseUniversity.ExecuteNonQuery($"CALL add_faculty('{textBoxes[0].Text.Trim()}', {getIdTable["Members"][comboBoxes["Members"].SelectedIndex]});");
                    else
                        databaseUniversity.ExecuteNonQuery($"CALL update_faculty({idStr}, '{textBoxes[0].Text.Trim()}', {getIdTable["Members"][comboBoxes["Members"].SelectedIndex]});");


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.Substring(6), "Ошибка!");
                }

                this.Close();
            }
            

            databaseUniversity.Close();

            
        } 

        private void Member_Click(object sender, RoutedEventArgs e)
        {
            databaseUniversity.Open();
            try
            {
                if (idStr == -1)
                    databaseUniversity.ExecuteNonQuery($"CALL add_departmentmember('{textBoxes[0].Text.Trim()}', '{textBoxes[1].Text.Trim()}', '{textBoxes[4].Text.Trim()}', " +
                                                                         $"'{textBoxes[2].Text.Trim()}', '{textBoxes[3].Text.Trim()}');");
                else
                    databaseUniversity.ExecuteNonQuery($"CALL update_departmentmember({idStr}, '{textBoxes[0].Text.Trim()}', '{textBoxes[1].Text.Trim()}', '{textBoxes[4].Text.Trim()}', " +
                                                                         $"'{textBoxes[2].Text.Trim()}', '{textBoxes[3].Text.Trim()}');");

                this.Close();
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message.Substring(6), "Ошибка!");
            }
            databaseUniversity.Close();


        } 

        private void Kafedra_Click(object sender, RoutedEventArgs e)
        {         
            databaseUniversity.Open();
            try
            {
                if (idStr == -1)
                    databaseUniversity.ExecuteNonQuery($"CALL add_kafedra('{textBoxes[0].Text.Trim()}', " +
                                               $"{getIdTable["Members"][comboBoxes["Members"].SelectedIndex]}, {getIdTable["Faculty"][comboBoxes["Faculty"].SelectedIndex]});");
                else
                    databaseUniversity.ExecuteNonQuery($"CALL update_kafedra({idStr}, '{textBoxes[0].Text.Trim()}', " +
                                              $"{getIdTable["Members"][comboBoxes["Members"].SelectedIndex]}, {getIdTable["Faculty"][comboBoxes["Faculty"].SelectedIndex]});");

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Substring(6), "Ошибка!");
            }
            databaseUniversity.Close();
        } 

        private void Discip_Click(object sender, RoutedEventArgs e)
        {
            databaseUniversity.Open();

            try
            {
                if (idStr == -1)
                    databaseUniversity.ExecuteNonQuery($"CALL add_discipline('{textBoxes[0].Text.Trim()}', {getIdTable["Kafedr"][comboBoxes["Kafedr"].SelectedIndex]})");
                else
                    databaseUniversity.ExecuteNonQuery($"CALL update_discipline({idStr}, '{textBoxes[0].Text.Trim()}', {getIdTable["Kafedr"][comboBoxes["Kafedr"].SelectedIndex]})");

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Substring(6), "Ошибка!");
            }

            databaseUniversity.Close();
        } 

        private void Special_Click(object sender, RoutedEventArgs e) 
        {
            databaseUniversity.Open();

            try
            {
                if (idStr == -1)
                    databaseUniversity.ExecuteNonQuery($"CALL add_speciality('{textBoxes[0].Text.Trim()}', '{textBoxes[1].Text.Trim()}', " +
                                               $"{getIdTable["Faculty"][comboBoxes["Faculty"].SelectedIndex]})");
                else
                    databaseUniversity.ExecuteNonQuery($"CALL update_speciality({idStr}, '{textBoxes[0].Text.Trim()}', '{textBoxes[1].Text.Trim()}', " +
                                              $"{getIdTable["Faculty"][comboBoxes["Faculty"].SelectedIndex]})");

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Substring(6), "Ошибка!");
            }

            databaseUniversity.Close();          
        } 

        private void Uchet_Click(object sender, RoutedEventArgs e)
        {
            string lec = textBoxes[0].Text == "" ? "0" : textBoxes[0].Text;
            string prac = textBoxes[1].Text == "" ? "0" : textBoxes[1].Text;
            string lab = textBoxes[2].Text == "" ? "0" : textBoxes[2].Text;
            if (!(int.TryParse(lab, out int a) && int.TryParse(lec, out int b) && int.TryParse(prac, out int c) && int.TryParse(textBoxes[3].Text, out int g)))
            {
                MessageBox.Show("Недопустимые значения!");
                return;
            }
            databaseUniversity.Open();
            try
            {
                if (idStr == -1)
                    databaseUniversity.ExecuteNonQuery($"CALL add_discipline_speciality('{getIdTable["Discip"][comboBoxes["Discip"].SelectedIndex]}', " +
                    $"'{getIdTable["Special"][comboBoxes["Special"].SelectedIndex]}', {textBoxes[3].Text.Trim()}, '{comboBoxes["Sessia"].SelectedIndex+1}', " +
                    $"{lec}, {prac}, {lab})");
                else
                    databaseUniversity.ExecuteNonQuery($"CALL update_discipline_speciality({idStr}, '{getIdTable["Discip"][comboBoxes["Discip"].SelectedIndex]}', " +
                    $"'{getIdTable["Special"][comboBoxes["Special"].SelectedIndex]}', {textBoxes[3].Text.Trim()}, '{comboBoxes["Sessia"].SelectedIndex + 1}', " +
                    $"{lec}, {prac}, {lab})");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()/*.Message.Substring(6)*/, "Ошибка!");
            }
            
            databaseUniversity.Close();    
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

            if (idStr == -1)
                button.Content = "Добавить";
            else
                button.Content = "Изменить";

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
