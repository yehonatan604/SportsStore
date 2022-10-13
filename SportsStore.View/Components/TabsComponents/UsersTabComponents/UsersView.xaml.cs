using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SportsStore.Controller;
using SportsStore.Enums;

namespace SportsStore.View.Themes.CustomControls.UsersTabComponents
{
    public partial class UsersView : UserControl
    {
        public static UsersView? Current { get; private set; }
        UserEntityHandler handler;
        public UsersView()
        {
            InitializeComponent();
            handler = new();
            Current = this;
        }

        
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dGrid = MainWindow.Current.Dgrid4;

            // check what is the search requirements
            string userType = ((int)Enum.Parse(typeof(UserTypes), BoxByUserType.Text)).ToString();

            bool byType = !string.IsNullOrEmpty(BoxByUserType.Text);
            bool bySaleDate = !string.IsNullOrEmpty(BoxBySaleDate.Text);
            bool byHireYear = !string.IsNullOrEmpty(BoxByHireYear.Text);

            dGrid.ItemsSource = 
                byType ? handler.GetTable("ByType", userType).ToList() :
                bySaleDate ? handler.GetTable("bySaleDate", BoxBySaleDate.Text).ToList() :
                byHireYear ? handler.GetTable("byHireYear", BoxByHireYear.Text).ToList() :
                byType && bySaleDate ? handler.GetTable("ByType_SaleDate", userType, BoxBySaleDate.Text).ToList() :
                byType && byHireYear ? handler.GetTable("ByType_HireYear", userType, BoxByHireYear.Text).ToList() :
                bySaleDate && byHireYear ? handler.GetTable("BySaleDate_HireYear", BoxBySaleDate.Text, BoxByHireYear.Text).ToList() :
                byType && bySaleDate && byHireYear ? handler.GetTable("ByAll", userType, BoxBySaleDate.Text, BoxByHireYear.Text).ToList() :
                handler.GetTable("Users").ToList();

            handler.AddSearchLog();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            BoxByUserType.Text = string.Empty;
            BoxBySaleDate.Text = string.Empty;
            BoxByHireYear.Text = string.Empty;
            BoxById.Text = string.Empty;
            BoxByName.Text = string.Empty;
        }

        private void BoxById_KeyUp(object sender, KeyEventArgs e)
        {
            MainWindow.Current.Dgrid4.ItemsSource = handler.Search("Id", BoxById.Text).ToList();
        }

        private void BoxSearch_KeyUp(object sender, KeyEventArgs e)
        {
            MainWindow.Current.Dgrid4.ItemsSource = handler.Search("Name", BoxByName.Text).ToList();
        }
    }
}
