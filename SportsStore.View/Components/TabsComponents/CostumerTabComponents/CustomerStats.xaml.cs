using SportsStore.Controller;
using System.Windows.Controls;

namespace SportsStore.View.Themes.CustomControls.CostumerTabCComponents
{
    public partial class CustomerStats : UserControl
    {
        public static CustomerStats? Current { get; private set; }

        public CustomerStats()
        {
            InitializeComponent();
            Current = this;

        }
        public void CollectCharts()
        {
            CustomerEntityHandler handler = new();
            BoxSMostSpent.Text = handler.GetStats()[0];
            BoxMostReturning.Text = handler.GetStats()[1];
            BoxMostVeteran.Text = handler.GetStats()[2];
            BoxMostNew.Text = handler.GetStats()[3];
        }
    }
}
