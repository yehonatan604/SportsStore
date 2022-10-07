using System;
using System.Windows;
using System.Windows.Controls;
using SportsStore.Controller;
using SportsStore.Utilities;

namespace SportsStore.View.Themes.CustomControls.EditStockTabComponents
{
    public partial class EditStockEditItem : UserControl
    {
        public static EditStockEditItem? Current { get; private set; }
        public EditStockEditItem()
        {
            InitializeComponent();
            Current = this;
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
                DGridController.RefreshDgrid();
            }
        }

        private void BoxItemType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditStockAddItem.Current.GlobalBoxItemType_SelectionChanged(sender);
        }

        private void BoxItemInnerType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditStockAddItem.Current.GlobalBoxItemInnerType_SelectionChanged(sender);
        }
    }
}
