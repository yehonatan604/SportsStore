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
        private readonly Read read = MainWindow.Current.read;
        public static UsersView? Current { get; private set; }
        public UsersView()
        {
            Current = this;
            InitializeComponent();
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
                byType ? read.GetUsers("ByType", userType).ToList() :
                bySaleDate ? read.GetUsers("bySaleDate", BoxBySaleDate.Text).ToList() :
                byHireYear ? read.GetUsers("byHireYear", BoxByHireYear.Text).ToList() :
                byType && bySaleDate ? read.GetUsers("ByTypeSaleDate", userType, BoxBySaleDate.Text).ToList() :
                byType && byHireYear ? read.GetUsers("ByTypeHireYear", userType, BoxByHireYear.Text).ToList() :
                bySaleDate && byHireYear ? read.GetUsers("BySaleDateHireYear", BoxBySaleDate.Text, BoxByHireYear.Text).ToList() :
                byType && bySaleDate && byHireYear ? read.GetUsers("BySaleDateHireYear", userType, BoxBySaleDate.Text, BoxByHireYear.Text).ToList() :
                read.GetTable("Users").ToList();
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
            MainWindow.Current.Dgrid4.ItemsSource = read.UsersSearchId(BoxById.Text).ToList();
        }

        private void BoxSearch_KeyUp(object sender, KeyEventArgs e)
        {
            MainWindow.Current.Dgrid4.ItemsSource = read.UsersSearchName(BoxByName.Text).ToList();
        }
    }
}
