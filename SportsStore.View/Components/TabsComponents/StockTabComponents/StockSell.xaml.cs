using SportsStore.Controller;
using SportsStore.Utilities;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SportsStore.View.Themes.CustomControls
{
    public partial class StockSell : UserControl
    {
        public static StockSell? Current { get; private set; }

        public StockSell()
        {
            InitializeComponent();
            FillCustomers();
            Current = this;
        }

        private void FillCustomers()
        {
            CustomerEntityHandler handler = new();
            foreach (string costumer in handler.GetList("ByCustomer"))
            {
                cmboBoxSellCustomer.Items.Add(costumer);
            }
        }
        private void BtnSell_Click(object sender, RoutedEventArgs e)
        {
            SaleEntityHandler handler = new();
            handler.Add(TboxSellID.Text, TboxSellQuantity.Text, cmboBoxSellCustomer.Text);
            MessageBox.Show("Sale!");
        }
        private void BoxItemPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            //Validate.IsStringNaN((TextBox)sender, e);
        }

    }
}
