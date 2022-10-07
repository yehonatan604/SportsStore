using SportsStore.Controller;
using SportsStore.Utilities;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SportsStore.View.Themes.CustomControls
{
    public partial class StockSell : UserControl
    {
        private readonly Create write = MainWindow.Current.write;
        private readonly Read read = MainWindow.Current.read;
        public static StockSell? Current { get; private set; }

        public StockSell()
        {
            InitializeComponent();
            Current = this;
            foreach (string costumer in read.GetList("ByCostumer"))
            {
                cmboBoxSellCustomer.Items.Add(costumer);
            }
        }
        private void BtnSell_Click(object sender, RoutedEventArgs e)
        {
            write.AddSale(Convert.ToInt16(TboxSellID.Text), Convert.ToInt16(TboxSellQuantity.Text), Convert.ToInt16(cmboBoxSellCustomer.Text));
            MessageBox.Show("Sale!");
        }
        private void BoxItemPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            //Validate.IsStringNaN((TextBox)sender, e);
        }

    }
}
