using SportsStore.Controller;
using SportsStore.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SportsStore.View
{
    public partial class LogInWindow : Window
    {
        private readonly Read reader;
        private readonly Create writer;

        public LogInWindow()
        {
            InitializeComponent();
            reader = new();
            writer = new();
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
            if (textBlock.Name == TxtBoxNames.TxtIForgot.ToString())
            {
                MessageBox.Show($"Email was sent to {BoxEmail.Text}");
            }
            if (textBlock.Name == TxtBoxNames.TxtKeepLogged.ToString())
            {
                ChkBoxRemember.IsChecked = !ChkBoxRemember.IsChecked;
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
            if (reader.CheckLogin(BoxEmail.Text, BoxPassword.Password))
            {
                Create.ChangeLoggedUserEmail(BoxEmail.Text);
                MessageBox.Show($"{BoxEmail.Text} Logged in Succesfully");

                if (ChkBoxRemember.IsChecked == true)
                {
                    Create.IsRememberMe = true;
                }
                Close();
            }
            else
            {
                MessageBox.Show("Wrong Email or Password, please try again");
            }
        }
        private void OnExit(object sender, System.ComponentModel.CancelEventArgs e)
        {
            writer.OnExitProgram();
        }
    }
}
