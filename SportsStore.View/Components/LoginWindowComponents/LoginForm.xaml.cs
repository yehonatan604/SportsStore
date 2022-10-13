using SportsStore.Controller;
using SportsStore.Enums;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SportsStore.View.Components.LoginWindowComponents
{
    public partial class LoginForm : UserControl
    {
        public static LoginForm Current;
        public LoginForm()
        {
            InitializeComponent();
            Current = this;
        }
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;

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
    }
}
