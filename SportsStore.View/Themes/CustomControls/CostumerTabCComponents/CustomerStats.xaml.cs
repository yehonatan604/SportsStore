using SportsStore.Controller;
using SportsStore.View.Themes.CustomControls.SalesTabComponents;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace SportsStore.View.Themes.CustomControls.CostumerTabCComponents
{
    /// <summary>
    /// Interaction logic for CustomerStats.xaml
    /// </summary>
    public partial class CustomerStats : UserControl
    {
        public static CustomerStats Instance;
        private readonly Read reader;
        public CustomerStats()
        {
            InitializeComponent();
            reader = new();
            Instance = this;

        }
        public void CollectCharts()
        {
            BoxSMostSpent.Text = reader.GetCustomerStats()[0];
            BoxMostReturning.Text = reader.GetCustomerStats()[1];
            BoxMostVeteran.Text = reader.GetCustomerStats()[2];
            BoxMostNew.Text = reader.GetCustomerStats()[3];
        }
    }
}
