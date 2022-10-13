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
        public static LogsView? Current { get; private set; }
        private readonly LogEntityHandler handler;
        public LogsView()
        {
            InitializeComponent();
            Current = this;
            handler = new();
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
                byUserType ? handler.GetTable("ByUserType", userType).ToList() :
                byActionType ? handler.GetTable("ByActionType", BoxByActionType.Text).ToList() :
                byActionDate ? handler.GetTable("ByActionDate", BoxByActionDate.Text).ToList() :
                byUserType && byActionType ? handler.GetTable("ByUserType_ActionType", userType, BoxByActionType.Text).ToList() :
                byUserType && byActionDate ? handler.GetTable("ByUserType_Date", userType, BoxByActionDate.Text).ToList() :
                byActionType && byActionDate ? handler.GetTable("ByActionType_Date", BoxByActionType.Text, BoxByActionDate.Text).ToList() :
                byUserType && byActionType && byActionDate ? handler.GetTable("ByAll", userType, BoxByActionType.Text, BoxByActionDate.Text).ToList() :
                handler.GetTable("Users").ToList();

            handler.AddSearchLog();
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
            MainWindow.Current.Dgrid5.ItemsSource = handler.Search("ById", BoxById.Text).ToList();
        }
        private void BoxSearch_KeyUp(object sender, KeyEventArgs e)
        {
            MainWindow.Current.Dgrid5.ItemsSource = handler.Search("ByName", BoxById.Text).ToList();
        }
    }
}
