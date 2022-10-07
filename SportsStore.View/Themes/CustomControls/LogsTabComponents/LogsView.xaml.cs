using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SportsStore.Controller;
using SportsStore.Enums;

namespace SportsStore.View.Themes.CustomControls.LogsTabComponents
{
    public partial class LogsView : UserControl
    {
        private readonly Read read = MainWindow.Current.read;
        public static LogsView? Current { get; private set; }
        public LogsView()
        {
            InitializeComponent();
            Current = this;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dGrid = MainWindow.Current.Dgrid5;
            string userType = string.Empty;

            // check what is the search requirements
            
            if (BoxByUserType.Text != string.Empty)
            {
                userType = ((int)Enum.Parse(typeof(UserTypes), BoxByUserType.Text)).ToString();
            } 
            bool byUserType = !string.IsNullOrEmpty(BoxByUserType.Text);
            bool byActionType = !string.IsNullOrEmpty(BoxByActionType.Text);
            bool byActionDate = !string.IsNullOrEmpty(BoxByActionDate.Text);

            dGrid.ItemsSource =
                byUserType ? read.GetLogs("ByUserType", userType).ToList() :
                byActionType ? read.GetLogs("ByActionType", BoxByActionType.Text).ToList() :
                byActionDate ? read.GetLogs("ByActionDate", BoxByActionDate.Text).ToList() :
                byUserType && byActionType ? read.GetLogs("ByUserTypeAction", userType, BoxByActionType.Text).ToList() :
                byUserType && byActionDate ? read.GetLogs("ByUserTypeDate", userType, BoxByActionDate.Text).ToList() :
                byActionType && byActionDate ? read.GetLogs("ByActionTypeDate", BoxByActionType.Text, BoxByActionDate.Text).ToList() :
                byUserType && byActionType && byActionDate ? read.GetLogs("ByAll", userType, BoxByActionType.Text, BoxByActionDate.Text).ToList() :
                read.GetTable("Users").ToList();
        }
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            BoxByUserType.Text = string.Empty;
            BoxByActionType.Text = string.Empty;
            BoxByActionDate.Text = string.Empty;
            BoxById.Text = string.Empty;
            BoxByName.Text = string.Empty;
        }
        private void BoxById_KeyUp(object sender, KeyEventArgs e)
        {
            MainWindow.Current.Dgrid5.ItemsSource = read.LogsSearchId(BoxById.Text).ToList();
        }
        private void BoxSearch_KeyUp(object sender, KeyEventArgs e)
        {
            MainWindow.Current.Dgrid5.ItemsSource = read.LogsSearchName(BoxByName.Text).ToList();
        }
    }
}
