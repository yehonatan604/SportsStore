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
    /// Interaction logic for SalesChart.xaml
    /// </summary>
    public partial class SalesChart : UserControl
    {
        public static SalesChart Instance;
        private readonly Read reader;
        public SalesChart()
        {
            InitializeComponent();
            reader = new();
            Instance = this;
        }

        public void CollectCharts()
        {
            BoxItem.Text = reader.GetSalesStatistics()[0];
            BoxItemType.Text = reader.GetSalesStatistics()[1];
            BoxItemInnerType.Text = reader.GetSalesStatistics()[2];
            BoxItemColor.Text = reader.GetSalesStatistics()[3];
            BoxItemSize.Text = reader.GetSalesStatistics()[4];
            BoxSalesman.Text = reader.GetSalesStatistics()[5];
            BoxDate.Text = reader.GetSalesStatistics()[6];
        }
    }
}
