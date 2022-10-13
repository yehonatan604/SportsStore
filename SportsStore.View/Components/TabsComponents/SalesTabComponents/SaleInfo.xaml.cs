using System.Windows.Controls;

namespace SportsStore.View.Themes.CustomControls.SalesTabComponents
{
    public partial class SaleInfo : UserControl
    {
        public static SaleInfo? Current { get; private set; }
        public SaleInfo()
        {
            InitializeComponent();
            Current = this;
        }
    }
}
