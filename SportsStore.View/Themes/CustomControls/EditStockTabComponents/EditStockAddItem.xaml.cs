using SportsStore.Controller;
using SportsStore.Enums;
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

namespace SportsStore.View.Themes.CustomControls.EditStockTabComponents
{
    public partial class EditStockAddItem : UserControl
    {
        Write writer;
        public static EditStockAddItem Instance;
        public EditStockAddItem()
        {
            InitializeComponent();
            Instance = this;
            writer = new();
        }

        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            _ = BoxItemType.Text == ItemTypes.Clothe.ToString() ?
                MessageBox.Show(writer.AddItem(BoxItemType.Text, BoxItemName.Text, Convert.ToDouble(BoxItemPrice.Text),
                                               BoxColor.Text, BoxItemInnerType.Text, BoxSize.Text)
            ? "Item Added Succesfuly" : "Operation Failed") :
                MessageBox.Show(writer.AddItem(BoxItemType.Text, BoxItemName.Text, Convert.ToDouble(BoxItemPrice.Text),
                                               BoxColor.Text, BoxItemInnerType.Text)
                                               ? "Item Added Succesfuly" : "Operation Failed");
            MainWindow.Instance.RefreshDgrid.Invoke();
        }
        private void BtnAddStock_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(BoxId.Text))
            {
                MessageBox.Show(writer.AddStock(Convert.ToInt16(BoxId.Text), Convert.ToInt16(BoxQuantity.Text))
                ? "Stock Added Succesfuly" : "Operation Failed");

                MainWindow.Instance.RefreshDgrid.Invoke();
            }
        }
        private void BoxItemType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GlobalBoxItemType_SelectionChanged(sender);
        }
        private void BoxItemInnerType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GlobalBoxItemInnerType_SelectionChanged(sender);
        }
        
        public void GlobalBoxItemType_SelectionChanged(object sender)
        {
            if (sender is ComboBox boxItemType)
            {
                BoxItemInnerType.Items.Clear();
                EditStockEditItem.Instance.BoxItemInnerType.Items.Clear();
                BoxItemInnerType.IsEnabled = true;

                switch (boxItemType.SelectedItem)
                {
                    case ItemTypes.Clothe:
                        {
                            BoxColor.IsEnabled = true;
                            BoxSize.IsEnabled = true;
                            CmboBoxFiller.Fill(new ColorTypes(), BoxColor);
                            CmboBoxFiller.Fill(new ColorTypes(), EditStockEditItem.Instance.BoxColor);
                            CmboBoxFiller.Fill(new ClotheTypes(), BoxItemInnerType);
                            CmboBoxFiller.Fill(new ClotheTypes(), EditStockEditItem.Instance.BoxItemInnerType);
                            break;
                        }
                    case ItemTypes.Ball:
                        {
                            BoxColor.IsEnabled = true;
                            CmboBoxFiller.Fill(new BallTypes(), BoxItemInnerType);
                            CmboBoxFiller.Fill(new BallTypes(), EditStockEditItem.Instance.BoxItemInnerType);
                            break;
                        }
                    case ItemTypes.Bat:
                        {
                            CmboBoxFiller.Fill(new BatTypes(), BoxItemInnerType);
                            CmboBoxFiller.Fill(new BatTypes(), EditStockEditItem.Instance.BoxItemInnerType);
                            break;
                        }
                    case ItemTypes.Net:
                        {
                            CmboBoxFiller.Fill(new NetTypes(), BoxItemInnerType);
                            CmboBoxFiller.Fill(new NetTypes(), EditStockEditItem.Instance.BoxItemInnerType);
                            break;
                        }
                }
            }
        }
        public void GlobalBoxItemInnerType_SelectionChanged(object sender)
        {
            if (sender is ComboBox boxItemInnerType)
            {
                switch (boxItemInnerType.SelectedItem)
                {
                    case ClotheTypes.Shirt:
                        {
                            CmboBoxFiller.FillSizeTypes(ClotheTypes.Shirt);
                            break;
                        }
                    case ClotheTypes.Pants:
                        {
                            CmboBoxFiller.FillSizeTypes(ClotheTypes.Pants);
                            break;
                        }
                    case ClotheTypes.Shorts:
                        {
                            CmboBoxFiller.FillSizeTypes(ClotheTypes.Shorts);
                            break;
                        }
                    case ClotheTypes.Shoes:
                        {
                            CmboBoxFiller.FillSizeTypes(ClotheTypes.Shoes);
                            break;
                        }
                }
            }
        }
    }
}
