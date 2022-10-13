using SportsStore.Controller;
using SportsStore.Model.Items;
using SportsStore.Utilities;
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

namespace SportsStore.View.Components.SettingsWindowComponents
{
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }
        
        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is TextBlock tb)
            {
                tb.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is TextBlock tb)
            {
                tb.Foreground = new SolidColorBrush(Colors.Magenta);
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show($"This will Create records for simulation, proceed?", "Generate Records",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                new UserEntityHandler().Generate();
                new CustomerEntityHandler().Generate();
                new StockEntityHandler().Generate();
                new SaleEntityHandler().Generate();
            }
        }
    }
}
