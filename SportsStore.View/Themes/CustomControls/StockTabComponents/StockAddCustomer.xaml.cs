using SportsStore.Controller;
using SportsStore.View.Utilities;
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

namespace SportsStore.View.Themes.CustomControls
{
    /// <summary>
    /// Interaction logic for StockAddCustomer.xaml
    /// </summary>
    public partial class StockAddCustomer : UserControl
    {
        Create writer = new();
        public StockAddCustomer()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            writer.AddCustomer(TBoxAddFname.Text, TBoxAddLname.Text, TBoxAddEmail.Text, Convert.ToDateTime(DPickerAddDOB.SelectedDate));
        }
    }
}
