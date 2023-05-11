using System;
using System.Collections.Generic;
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
        public AddMember(int ind, PostgresDataBase databaseUniversity)
        {
            this.databaseUniversity = databaseUniversity;

            InitializeComponent();     

            if (ind == 3)
            {
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
                
                textBoxPanel.Children.Add(labels[0]);
                textBoxPanel.Children.Add(textBoxes[0]);

                textBoxPanel.Children.Add(labels[1]);
                textBoxPanel.Children.Add(textBoxes[1]);

                textBoxPanel.Children.Add(labels[2]);
                textBoxPanel.Children.Add(textBoxes[2]);

                textBoxPanel.Children.Add(labels[3]);
                textBoxPanel.Children.Add(textBoxes[3]);

                textBoxPanel.Children.Add(labels[4]);
                textBoxPanel.Children.Add(textBoxes[4]);

                Button addMembers = new Button();
                addMembers.Content = "Добавить";
                addMembers.Margin = new Thickness(10, 0, 10, 10);
                addMembers.Width = 300;
                addMembers.Height = 50;
                addMembers.Click += AddMembers_Click;
                    
                textBoxPanel.Children.Add(addMembers);                
            }
        }

        private void AddMembers_Click(object sender, RoutedEventArgs e)
        {
            databaseUniversity.Open();
            databaseUniversity.ExecuteNonQuery($"CALL add_departmentmember('{textBoxes[0].Text}', '{textBoxes[1].Text}', '{textBoxes[2].Text}', '{textBoxes[3].Text}', '{textBoxes[4].Text}');");
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
    }
}
