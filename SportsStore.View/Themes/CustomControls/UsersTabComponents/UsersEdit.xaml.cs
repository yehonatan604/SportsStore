using System;
using System.Windows;
using System.Windows.Controls;
using SportsStore.Controller;
using SportsStore.Enums;
using SportsStore.View.Utilities;

namespace SportsStore.View.Themes.CustomControls.UsersTabComponents
{
    public partial class UsersEdit : UserControl
    {
        private readonly Create writer;
        private readonly Read reader;
        private readonly Delete deleter;
        private readonly Update update;

        public static UsersEdit Instatnce;
        public UsersEdit()
        {
            InitializeComponent();
            writer = new();
            reader = new();
            deleter = new();
            update = new();
            Instatnce = this;

            // Fill BoxUserId
            foreach (string date in reader.GetList("Users"))
            {
                BoxUserId.Items.Add(date);
            }
        }
        
        // Event Handlers
        private void TboxChangeMail_LostFocus(object sender, RoutedEventArgs e)
        {
            Validate.IsEmailValid(BoxUserEmail);
        }
        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(BoxUserType.Text))
            {
                ChangeUserType(BoxUserType.Text);
            }
            if (!string.IsNullOrEmpty(BoxUserEmail.Text))
            {
                ChangeEmail();
            }
            if (!string.IsNullOrEmpty(BoxHireDate.Text))
            {
                ChangeHireDate();
            }
        }
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"This will reset user password to '12345'.\n " +
                                 "are you sure)?", "Change User Password",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                update.UserPassword(Convert.ToInt16(BoxUserId), Md5Hash.Create("12345"));
            }
        }
        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"This will remove user!.\n " +
                                 "are you sure)?", "Remove User",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                deleter.RemoveUser(Convert.ToInt16(BoxUserId.Text));
                MainWindow.Instance.RefreshDgrid(MainWindow.Instance.Dgrid4);
            }
        }
        private void BtnFreeze_Click(object sender, RoutedEventArgs e)
        {
            ChangeUserType(UserTypes.No_User.ToString());
        }

        // Edit User Methods
        private void ChangeUserType(string userType)
        {
            if (MessageBox.Show($"This will change the user type to {userType}.\n " +
                                 "are you sure)?", "Change User Type",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                update.UserType(Convert.ToInt16(BoxUserId.Text), userType);
                MainWindow.Instance.RefreshDgrid(MainWindow.Instance.Dgrid4);
            }

        }
        private void ChangeEmail()
        {
            if (MessageBox.Show($"This will change user email to {BoxUserEmail.Text}.\n " +
                                 "are you sure)?", "Change User Email",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                update.UserEmail(Convert.ToInt16(BoxUserId.Text), BoxUserEmail.Text);
                MainWindow.Instance.RefreshDgrid(MainWindow.Instance.Dgrid4);
            }
        }
        private void ChangeHireDate()
        {
            if (MessageBox.Show($"This will change user hire date.\n " +
                                 "are you sure)?", "Change User hire date",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                update.UserHireDate(Convert.ToInt16(BoxUserId), BoxHireDate.Text);
            }
        }
    }
}
