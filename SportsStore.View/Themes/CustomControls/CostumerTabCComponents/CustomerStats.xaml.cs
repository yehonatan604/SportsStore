using SportsStore.Controller;
using System.Windows.Controls;

namespace SportsStore.View.Themes.CustomControls.CostumerTabCComponents
{
    public partial class CustomerStats : UserControl
    {
        public static CustomerStats? Current { get; private set; }
        public readonly Read read = MainWindow.Current.read;

        public CustomerStats()
        {
            InitializeComponent();
            Current = this;

        }
        public void CollectCharts()
        {
            BoxSMostSpent.Text = read.GetCustomerStats()[0];
            BoxMostReturning.Text = read.GetCustomerStats()[1];
            BoxMostVeteran.Text = read.GetCustomerStats()[2];
            BoxMostNew.Text = read.GetCustomerStats()[3];
        }
    }
}
