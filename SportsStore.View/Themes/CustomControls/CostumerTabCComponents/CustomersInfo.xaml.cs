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

namespace SportsStore.View.Themes.CustomControls.CostumerTabCComponents
{
    /// <summary>
    /// Interaction logic for CustomersInfo.xaml
    /// </summary>
    public partial class CustomersInfo : UserControl
    {
        public static CustomersInfo Instance;
        public CustomersInfo()
        {
            InitializeComponent();
            Instance = this;
        }
    }
}
