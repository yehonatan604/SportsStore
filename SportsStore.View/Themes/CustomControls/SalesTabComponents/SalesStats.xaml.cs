using System.Windows.Controls;
using SportsStore.Controller;

namespace SportsStore.View.Themes.CustomControls.SalesTabComponents
{
    public partial class SalesStats : UserControl
    {
        public static SalesStats? Current { get; private set; }
        private readonly Read read = MainWindow.Current.read;
        public SalesStats()
        {
            InitializeComponent();
            Current = this;
        }

        public void CollectCharts()
        {
            BoxItem.Text = read.GetSalesStats()[0];
            BoxItemType.Text = read.GetSalesStats()[1];
            BoxItemInnerType.Text = read.GetSalesStats()[2];
            BoxItemColor.Text = read.GetSalesStats()[3];
            BoxItemSize.Text = read.GetSalesStats()[4];
            BoxSalesman.Text = read.GetSalesStats()[5];
            BoxDate.Text = read.GetSalesStats()[6];
        }
    }
}
