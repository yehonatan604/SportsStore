﻿using System;
using System.Windows;
using System.Windows.Controls;
using SportsStore.Controller;
using SportsStore.Enums;
using SportsStore.Utilities;

namespace SportsStore.View.Themes.CustomControls.EditStockTabComponents
{
    public partial class EditStockAddItem : UserControl
    {
        private readonly Create write = MainWindow.Current.write;
        public static EditStockAddItem? Current { get; private set; }

        public EditStockAddItem()
        {
            InitializeComponent();
            Current = this;
        }

        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            _ = BoxItemType.Text == ItemTypes.Clothe.ToString() ?
                MessageBox.Show(write.AddItem(BoxItemType.Text, BoxItemName.Text, Convert.ToDouble(BoxItemPrice.Text),
                                               BoxColor.Text, BoxItemInnerType.Text, BoxSize.Text)
            ? "Item Added Succesfuly" : "Operation Failed") :
                MessageBox.Show(write.AddItem(BoxItemType.Text, BoxItemName.Text, Convert.ToDouble(BoxItemPrice.Text),
                                               BoxColor.Text, BoxItemInnerType.Text)
                                               ? "Item Added Succesfuly" : "Operation Failed");
            DGridController.RefreshDgrid();
        }
        private void BtnAddStock_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(BoxId.Text))
            {
                MessageBox.Show(write.AddStock(Convert.ToInt16(BoxId.Text), Convert.ToInt16(BoxQuantity.Text))
                ? "Stock Added Succesfuly" : "Operation Failed");

                DGridController.RefreshDgrid();
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
                EditStockEditItem.Current.BoxItemInnerType.Items.Clear();
                BoxItemInnerType.IsEnabled = true;

                switch (boxItemType.SelectedItem)
                {
                    case ItemTypes.Clothe:
                        {
                            BoxColor.IsEnabled = true;
                            BoxSize.IsEnabled = true;
                            CmboBoxFiller.Fill(new ColorTypes(), BoxColor);
                            CmboBoxFiller.Fill(new ColorTypes(), EditStockEditItem.Current.BoxColor);
                            CmboBoxFiller.Fill(new ClotheTypes(), BoxItemInnerType);
                            CmboBoxFiller.Fill(new ClotheTypes(), EditStockEditItem.Current.BoxItemInnerType);
                            break;
                        }
                    case ItemTypes.Ball:
                        {
                            BoxColor.IsEnabled = true;
                            CmboBoxFiller.Fill(new BallTypes(), BoxItemInnerType);
                            CmboBoxFiller.Fill(new BallTypes(), EditStockEditItem.Current.BoxItemInnerType);
                            break;
                        }
                    case ItemTypes.Bat:
                        {
                            CmboBoxFiller.Fill(new BatTypes(), BoxItemInnerType);
                            CmboBoxFiller.Fill(new BatTypes(), EditStockEditItem.Current.BoxItemInnerType);
                            break;
                        }
                    case ItemTypes.Net:
                        {
                            CmboBoxFiller.Fill(new NetTypes(), BoxItemInnerType);
                            CmboBoxFiller.Fill(new NetTypes(), EditStockEditItem.Current.BoxItemInnerType);
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
