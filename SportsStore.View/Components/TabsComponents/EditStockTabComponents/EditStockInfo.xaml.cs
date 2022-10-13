using System.Windows.Controls;

namespace SportsStore.View.Themes.CustomControls.EditStockTabComponents
{
    public partial class EditStockInfo : UserControl
    {
        public static EditStockInfo? Current { get; private set; }
        public EditStockInfo()
        {
            InitializeComponent();
            Current = this;
        }
    }
}
