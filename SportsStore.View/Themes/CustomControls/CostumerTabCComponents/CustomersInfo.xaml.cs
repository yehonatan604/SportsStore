using System.Windows.Controls;

namespace SportsStore.View.Themes.CustomControls.CostumerTabCComponents
{
    public partial class CustomersInfo : UserControl
    {
        public static CustomersInfo? Current { get; private set; }
        public CustomersInfo()
        {
            InitializeComponent();
            Current = this;
        }
    }
}
