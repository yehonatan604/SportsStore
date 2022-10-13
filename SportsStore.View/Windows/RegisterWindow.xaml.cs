using System.Windows;
using System.Windows.Controls;
using SportsStore.Controller;
using SportsStore.Utilities;
using SportsStore.Enums;

namespace SportsStore.View
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            UserEntityHandler handler = new();
            if (Validate.Registeration(BoxFname.Text, BoxLname.Text, UserTypes.SalesMan,
                                       BoxEmail.Text, BoxPassword.Password, BoxConfirm.Password))
            {
                handler.Add(BoxFname.Text, BoxLname.Text, UserTypes.SalesMan.ToString(),
                                BoxEmail.Text, Md5Hash.Create(BoxConfirm.Password));
                MessageBox.Show("User Registered Successfuly");
            }
            else
            {
                MessageBox.Show("Please make sure that all fields are full");
            }

        }

        private void BoxEmail_Check(object sender, RoutedEventArgs e)
        {
            Validate.IsEmailValid((TextBox)sender);
        }
    }
}
