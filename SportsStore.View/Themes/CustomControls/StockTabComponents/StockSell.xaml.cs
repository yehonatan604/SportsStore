using SportsStore.Controller;
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

namespace SportsStore.View.Themes.CustomControls
{
    /// <summary>
    /// Interaction logic for StockSell.xaml
    /// </summary>
    public partial class StockSell : UserControl
    {
        private readonly Create writer;
        private readonly Read reader;
        public static StockSell Instance;
        public StockSell()
        {
            InitializeComponent();
            writer = new();
            reader = new();
            Instance = this;
            foreach (string costumer in reader.GetList("ByCostumer"))
            {
                cmboBoxSellCustomer.Items.Add(costumer);
            }
        }
        private void BtnSell_Click(object sender, RoutedEventArgs e)
        {
            writer.AddSale(Convert.ToInt16(TboxSellID.Text), Convert.ToInt16(TboxSellQuantity.Text), Convert.ToInt16(cmboBoxSellCustomer.Text));
            MessageBox.Show("Sale!");
        }
        private void BoxItemPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            //Validate.IsStringNaN((TextBox)sender, e);
        }

    }
}
