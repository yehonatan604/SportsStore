using System.Windows;
using System.Windows.Controls;
using SportsStore.Controller;
using SportsStore.View.Utilities;
using SportsStore.Enums;

namespace SportsStore.View
{
    public partial class RegisterWindow : Window
    {
        private readonly Create writer;
        public RegisterWindow()
        {
            InitializeComponent();
            writer = new();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Validate.Registeration(BoxFname.Text, BoxLname.Text, UserTypes.SalesMan,
                                       BoxEmail.Text, BoxPassword.Password, BoxConfirm.Password))
            {
                MessageBox.Show(writer.AddNewUser(BoxFname.Text, BoxLname.Text, UserTypes.SalesMan,
                                BoxEmail.Text, Md5Hash.Create(BoxConfirm.Password)) ?
                                "User Added Succecfuly" :
                                "Operation failed, could not register user.");
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
