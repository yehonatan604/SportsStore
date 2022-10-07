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

namespace SportsStore.View.Themes.CustomControls.LogsTabComponents
{
    public partial class LogsView : UserControl
    {
        private readonly Read reader;
        public static LogsView Instance;
        public LogsView()
        {
            InitializeComponent();
            Instance = this;
            reader = new();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dGrid = MainWindow.Instance.Dgrid5;

            // check what is the search requirements
            string userType = BoxByUserType.Text ??= ((int)Enum.Parse(typeof(UserTypes), BoxByUserType.Text)).ToString();

            bool byUserType = !string.IsNullOrEmpty(BoxByUserType.Text);
            bool byActionType = !string.IsNullOrEmpty(BoxByActionType.Text);
            bool byActionDate = !string.IsNullOrEmpty(BoxByActionDate.Text);

            dGrid.ItemsSource =
                byUserType ? reader.GetLogs("ByUserType", userType).ToList() :
                byActionType ? reader.GetLogs("ByActionType", BoxByActionType.Text).ToList() :
                byActionDate ? reader.GetLogs("ByActionDate", BoxByActionDate.Text).ToList() :
                byUserType && byActionType ? reader.GetLogs("ByUserTypeAction", userType, BoxByActionType.Text).ToList() :
                byUserType && byActionDate ? reader.GetLogs("ByUserTypeDate", userType, BoxByActionDate.Text).ToList() :
                byActionType && byActionDate ? reader.GetLogs("ByActionTypeDate", BoxByActionType.Text, BoxByActionDate.Text).ToList() :
                byUserType && byActionType && byActionDate ? reader.GetLogs("ByAll", userType, BoxByActionType.Text, BoxByActionDate.Text).ToList() :
                reader.GetTable("Users").ToList();
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
            MainWindow.Instance.Dgrid5.ItemsSource = reader.LogsSearchId(BoxById.Text).ToList();
        }
        private void BoxSearch_KeyUp(object sender, KeyEventArgs e)
        {
            MainWindow.Instance.Dgrid5.ItemsSource = reader.LogsSearchName(BoxByName.Text).ToList();
        }
    }
}
