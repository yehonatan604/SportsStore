using SportsStore.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SportsStore.View.Themes.CustomControls
{
    /// <summary>
    /// Interaction logic for StockRefund.xaml
    /// </summary>
    public partial class StockRefund : UserControl
    {
        private readonly Write writer;
        private readonly Read reader;
        public StockRefund()
        {
            InitializeComponent();
            writer = new();
            reader = new();

            reader.GetList("ByCostumer").ForEach(costumer => CmbBoxCostumer.Items.Add(costumer));
        }

        private void CmbBoxCostumer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbBoxCostumer.SelectedItem is string cmb)
            {
                List<string> list = reader.GetList("ByCostumerDate", cmb);
                list.ForEach(date => CmbBoxSaleDate.Items.Add(date));
            }
        }

        private void CmbBoxSaleDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbBoxCostumer.SelectedItem is string cmb && CmbBoxSaleDate.SelectedItem is string date)
            {
                List<string> list = reader.GetList("SalesByDate", cmb, date);
                list.ForEach(sale => CmbBoxSale.Items.Add(sale));
            }
        }

        private void CmbBoxSale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}