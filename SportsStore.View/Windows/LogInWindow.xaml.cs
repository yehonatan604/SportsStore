using SportsStore.Controller;
using SportsStore.Enums;
using SportsStore.View.Components.LoginWindowComponents;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SportsStore.View
{
    public partial class LogInWindow : Window
    {
        public static LogInWindow Current;
        public LogInWindow()
        {
            InitializeComponent();
            Current = this;
        }
        //Shared event handlers
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;

            if (textBlock.Name == TxtBoxNames.TxtRegister.ToString())
            {
                RegisterWindow registerWindow = new();
                registerWindow.ShowDialog();
            }
        }
        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Foreground = new SolidColorBrush(Colors.DarkViolet);
        }
        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Foreground = new SolidColorBrush(Colors.Black);
        }

        //Specific event handlers
        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {
            LoginForm logInForm = LoginForm.Current;

            if (new UserEntityHandler().CheckLogin(logInForm.BoxEmail.Text, logInForm.BoxPassword.Password))
            {
                EntityHandler.LoggedUserEmail = logInForm.BoxEmail.Text;
                MessageBox.Show(EntityHandler.LoggedUserEmail);
                MessageBox.Show($"{logInForm.BoxEmail.Text} Logged in Succesfully");

                if (logInForm.ChkBoxRemember.IsChecked == true)
                {
                    EntityHandler.IsRememberMe = true;
                }
                Close();
            }
            else
            {
                MessageBox.Show("Wrong Email or Password, please try again");
            }
        }
        private void OnExit(object sender, CancelEventArgs e)
        {
            new UserEntityHandler().OnStartProgram();
        }
    }
}
