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
    /// Interaction logic for SalesStats.xaml
    /// </summary>
    public partial class SalesStats : UserControl
    {
        public static SalesStats Instance;
        private readonly Read reader;
        public SalesStats()
        {
            InitializeComponent();
            reader = new();
            Instance = this;
        }

        public void CollectCharts()
        {
            BoxItem.Text = reader.GetSalesStats()[0];
            BoxItemType.Text = reader.GetSalesStats()[1];
            BoxItemInnerType.Text = reader.GetSalesStats()[2];
            BoxItemColor.Text = reader.GetSalesStats()[3];
            BoxItemSize.Text = reader.GetSalesStats()[4];
            BoxSalesman.Text = reader.GetSalesStats()[5];
            BoxDate.Text = reader.GetSalesStats()[6];
        }
    }
}
