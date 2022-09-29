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

namespace SportsStore.View.Themes.CustomControls.EditStockTabComponents
{
    public partial class EditStockInfo : UserControl
    {
        public static EditStockInfo Instance;
        public EditStockInfo()
        {
            InitializeComponent();
            Instance = this;
        }
    }
}
