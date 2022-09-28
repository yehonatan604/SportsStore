using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Reflection.PortableExecutable;
using SportsStore.Enums;
using SportsStore.Controller;

namespace SportsStore.View.Utilities
{
    public static class Validate
    {
        public static bool Registeration(string firstName, string lastName,
                                         UserTypes userType, string email,
                                         string password, string confirmPassword)
        {
            return !(string.IsNullOrWhiteSpace(firstName) ||
                     string.IsNullOrWhiteSpace(lastName) ||
                     !Enum.IsDefined(userType) ||
                     string.IsNullOrWhiteSpace(email) ||
                     string.IsNullOrWhiteSpace(password) ||
                     password != confirmPassword);
        }
        public static bool ItemCreation(string itemName, string itemPrice,
                                        string itemType, string itemIneerType,
                                        string color, string size)
        {
            return !(string.IsNullOrWhiteSpace(itemName) ||
                     string.IsNullOrWhiteSpace(itemPrice) ||
                     string.IsNullOrWhiteSpace(itemType) ||
                     string.IsNullOrWhiteSpace(itemIneerType) ||
                     string.IsNullOrWhiteSpace(color) ||
                     string.IsNullOrWhiteSpace(size));
        }
        public static bool IsStringNaN(TextBox textBox, RoutedEventArgs e)
        {
            if (e.Handled = new Regex("[^0-9,.,0-9]+").IsMatch(textBox.Text))
            {
                MessageBox.Show("Please enter numbers only!!!");
                textBox.BorderBrush = new SolidColorBrush(Colors.IndianRed);
                return false;
            }
            textBox.BorderBrush = new SolidColorBrush(Colors.Transparent);
            return true;
        }
        public static bool IsEmailValid(TextBox textBox)
        {
            Read reader = new();
            Regex regex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(textBox.Text);
            if (!match.Success)
            {
                MessageBox.Show("Please enter valid email!!!");
                textBox.BorderBrush = new SolidColorBrush(Colors.IndianRed);
                return false;
            }
            else if (!reader.CheckAvailability(textBox.Text))
            {
                MessageBox.Show("Email is already taken!");
                textBox.BorderBrush = new SolidColorBrush(Colors.IndianRed);
                return false;
            }
            textBox.BorderBrush = new SolidColorBrush(Colors.Transparent);
            return true;
        }
    }
}
