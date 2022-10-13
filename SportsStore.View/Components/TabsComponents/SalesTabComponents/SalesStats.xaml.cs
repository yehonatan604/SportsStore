using System.Windows.Controls;
using SportsStore.Controller;

namespace SportsStore.View.Themes.CustomControls.SalesTabComponents
{
    public partial class SalesStats : UserControl
    {
        public static SalesStats? Current { get; private set; }
        public SalesStats()
        {
            InitializeComponent();
            Current = this;
        }

        public void CollectCharts()
        {
            SaleEntityHandler handler = new();
            BoxItem.Text = handler.GetStats()[0];
            BoxItemType.Text = handler.GetStats()[1];
            BoxItemInnerType.Text = handler.GetStats()[2];
            BoxItemColor.Text = handler.GetStats()[3];
            BoxItemSize.Text = handler.GetStats()[4];
            BoxSalesman.Text = handler.GetStats()[5];
            BoxDate.Text = handler.GetStats()[6];
        }
    }
}
