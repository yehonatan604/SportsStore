using SportsStore.Controller;
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

namespace SportsStore.View.Themes.CustomControls.SalesTabComponents
{
    /// <summary>
    /// Interaction logic for SaleViews.xaml
    /// </summary>
    public partial class SaleViews : UserControl
    {
        Read reader;
        public static SaleViews Instance;
        public SaleViews()
        {
            InitializeComponent();
            Instance = this;
            reader = new();
        }
        private void TboxByItem_KeyUp(object sender, KeyEventArgs e)
        {
            MainWindow.Instance.Dgrid2.ItemsSource = reader.GetTable("ByItemId", BoxById.Text).ToList();
        }
        private void TboxByItemType_DropDownClosed(object sender, EventArgs e)
        {
            MainWindow.Instance.Dgrid2.ItemsSource = reader.GetTable("ByType", CmbBoxByItemType.Text).ToList();
        }
        private void TboxBySalesman_DropDownClosed(object sender, EventArgs e)
        {
            MainWindow.Instance.Dgrid2.ItemsSource = reader.GetTable("BySalseMan", CmbBoxBySalesman.Text).ToList();
        }
        private void TboxByDate_DropDownClosed(object sender, EventArgs e)
        {
            MainWindow.Instance.Dgrid2.ItemsSource = reader.GetTable("ByDate", CmbBoxByDate.Text).ToList();
        }
        private void BtnMinMax_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Dgrid2.ItemsSource = reader.GetTable("ByTPrice", TboxMin.Text, TboxMax.Text).ToList();
        }
        private void TboxMin_GotFocus(object sender, RoutedEventArgs e)
        {
            TboxMin.Text = string.Empty;
        }
        private void TboxMax_GotFocus(object sender, RoutedEventArgs e)
        {
            TboxMax.Text = string.Empty;
        }
    }
}
