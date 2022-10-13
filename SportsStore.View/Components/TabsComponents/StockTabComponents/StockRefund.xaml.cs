using System.Windows.Controls;
using System.Collections.Generic;
using SportsStore.Controller;
using SportsStore.Enums;
using SportsStore.Utilities;

namespace SportsStore.View.Themes.CustomControls
{
    public partial class StockRefund : UserControl
    {
        private readonly CustomerEntityHandler customerHandler;
        private readonly SaleEntityHandler salesHandler;
        public StockRefund()
        {
            InitializeComponent();
            customerHandler = new();
            salesHandler = new();
            customerHandler.GetList("ByCustomer").ForEach(costumer => CmbBoxCostumer.Items.Add(costumer));
            CmboBoxFiller.Fill(new RefundTypes(), CmbBoxReason);
        }

        private void CmbBoxCostumer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbBoxCostumer.SelectedItem is string cmb)
            {
                List<string> list = customerHandler.GetList("ByCustomer_Date", cmb);
                list.ForEach(date => CmbBoxSaleDate.Items.Add(date));
            }
        }

        private void CmbBoxSaleDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbBoxCostumer.SelectedItem is string cmb && CmbBoxSaleDate.SelectedItem is string date)
            {
                //List<string> list = salesHandler.GetList("ByCustomer_Date", date,);
                //list.ForEach(sale => CmbBoxSale.Items.Add(sale));
            }
        }

        private void CmbBoxSale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbBoxCostumer.SelectedItem is string costumerId && CmbBoxSale.SelectedItem is string sale)
            {
                //List<string> list = read.GetList("SalesInfo", costumerId, sale);

                //StockInfo.Current.TboxInfo.Text = $"{list[0]} {list[1]}\n" +
                //                          $"Id: {costumerId}\n" +
                //                          $"Stock: {list[2]}\n" +
                //                          $"Quantity: {list[3]}\n" +
                //                          $"Total Price: {list[4]}$";
            }
        }
    }
}