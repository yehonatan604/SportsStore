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
                StockEntityHandler handler = new();
                handler.Update("Name", BoxItemName.Text);
                handler.Update("Price", BoxItemPrice.Text);
                handler.Update("Quantity", BoxQuantity.Text);
                handler.Update("ItemType", BoxItemType.Text);
                handler.Update("ItemInnerType", BoxItemInnerType.Text);
                handler.Update("ItemInnerType", BoxItemInnerType.Text);
                handler.Update("Color", BoxColor.Text);
                handler.Update("Size", BoxSize.Text);
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
