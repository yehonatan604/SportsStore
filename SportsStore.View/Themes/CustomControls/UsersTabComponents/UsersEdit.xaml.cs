using System;
using System.Windows;
using System.Windows.Controls;
using SportsStore.Controller;
using SportsStore.Enums;
using SportsStore.Utilities;

namespace SportsStore.View.Themes.CustomControls.UsersTabComponents
{
    public partial class UsersEdit : UserControl
    {
        private readonly Read read = MainWindow.Current.read;
        private readonly Delete delete = MainWindow.Current.delete;
        private readonly Update update = MainWindow.Current.update;

        public static UsersEdit? Current { get; private set; }
        public UsersEdit()
        {
            InitializeComponent();
            Current = this;

            // Fill BoxUserId
            foreach (string date in read.GetList("Users"))
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
                delete.RemoveUser(Convert.ToInt16(BoxUserId.Text));
                DGridController.RefreshDgrid(MainWindow.Current.Dgrid4);
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
                DGridController.RefreshDgrid(MainWindow.Current.Dgrid4);
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
                DGridController.RefreshDgrid(MainWindow.Current.Dgrid4);
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
