using System.Windows.Controls;

namespace SportsStore.View.Themes.CustomControls
{
    public partial class StockInfo : UserControl
    {
        public static StockInfo? Current { get; private set; }
        public StockInfo()
        {
            InitializeComponent();
            Current = this;
        }
    }
}
