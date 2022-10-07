using SportsStore.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    public partial class EditStockEditItem : UserControl
    {
        Create writer;
        public static EditStockEditItem Instance;
        public EditStockEditItem()
        {
            InitializeComponent();
            Instance = this;
            writer = new();
        }

        private void BtnEditItem_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(BoxItemId.Text))
            {
                MessageBox.Show(new Update().EditStock(Convert.ToInt16(BoxItemId.Text), BoxItemName.Text, 
                                                 BoxItemPrice.Text, BoxQuantity.Text, BoxItemType.Text, 
                                                 BoxItemInnerType.Text, BoxColor.Text, BoxSize.Text) ?
                                                 "Stock Details Was Changed Successfuly" : 
                                                 "Operation Failed");
                MainWindow.Instance.RefreshDgrid.Invoke();
            }
        }

        private void BoxItemType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditStockAddItem.Instance.GlobalBoxItemType_SelectionChanged(sender);
        }

        private void BoxItemInnerType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditStockAddItem.Instance.GlobalBoxItemInnerType_SelectionChanged(sender);
        }
    }
}
