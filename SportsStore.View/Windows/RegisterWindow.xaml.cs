using System.Windows;
using System.Windows.Controls;
using SportsStore.Controller;
using SportsStore.Utilities;
using SportsStore.Enums;
using SportsStore.View.Components.RegisterWindowComponents;

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
            RegistraionForm regForm = RegistraionForm.Instance;
            UserEntityHandler handler = new();
            if (Validate.Registeration(regForm.BoxFname.Text, regForm.BoxLname.Text, UserTypes.SalesMan,
                                       regForm.BoxEmail.Text, regForm.BoxPassword.Password, regForm.BoxConfirmPassword.Password))
            {
                handler.Add(regForm.BoxFname.Text, regForm.BoxLname.Text, UserTypes.SalesMan.ToString(),
                                regForm.BoxEmail.Text, Md5Hash.Create(regForm.BoxConfirmPassword.Password));
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
