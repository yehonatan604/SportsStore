using SportsStore.Controller;
using SportsStore.Enums;
using SportsStore.View.Utilities;
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

namespace SportsStore.View.Themes.CustomControls.UsersTabComponents
{
    /// <summary>
    /// Interaction logic for UsersView.xaml
    /// </summary>
    public partial class UsersView : UserControl
    {
        private readonly Read reader;
        public static UsersView Instance;
        public UsersView()
        {
            reader = new();
            Instance = this;
            InitializeComponent();
        }

        
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dGrid = MainWindow.Instance.Dgrid4;

            // check what is the search requirements
            string userType = ((int)Enum.Parse(typeof(UserTypes), BoxByUserType.Text)).ToString();

            bool byType = !string.IsNullOrEmpty(BoxByUserType.Text);
            bool bySaleDate = !string.IsNullOrEmpty(BoxBySaleDate.Text);
            bool byHireYear = !string.IsNullOrEmpty(BoxByHireYear.Text);

            dGrid.ItemsSource = 
                byType ? reader.GetUsers("ByType", userType).ToList() :
                bySaleDate ? reader.GetUsers("bySaleDate", BoxBySaleDate.Text).ToList() :
                byHireYear ? reader.GetUsers("byHireYear", BoxByHireYear.Text).ToList() :
                byType && bySaleDate ? reader.GetUsers("ByTypeSaleDate", userType, BoxBySaleDate.Text).ToList() :
                byType && byHireYear ? reader.GetUsers("ByTypeHireYear", userType, BoxByHireYear.Text).ToList() :
                bySaleDate && byHireYear ? reader.GetUsers("BySaleDateHireYear", BoxBySaleDate.Text, BoxByHireYear.Text).ToList() :
                byType && bySaleDate && byHireYear ? reader.GetUsers("BySaleDateHireYear", userType, BoxBySaleDate.Text, BoxByHireYear.Text).ToList() :
                reader.GetTable("Users").ToList();
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
            MainWindow.Instance.Dgrid4.ItemsSource = reader.UsersSearchId(BoxById.Text).ToList();
        }

        private void BoxSearch_KeyUp(object sender, KeyEventArgs e)
        {
            MainWindow.Instance.Dgrid4.ItemsSource = reader.UsersSearchName(BoxByName.Text).ToList();
        }
    }
}
