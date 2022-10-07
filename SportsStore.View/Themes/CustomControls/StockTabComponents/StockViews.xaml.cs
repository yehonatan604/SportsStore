using SportsStore.Controller;
using SportsStore.Enums;
using SportsStore.View.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SportsStore.View.Themes.CustomControls
{
    /// <summary>
    /// Interaction logic for StockViews.xaml
    /// </summary>
    public partial class StockViews : UserControl
    {
        private readonly Read reader = new();
        public static StockViews Instance;
        public StockViews()
        {
            InitializeComponent();
            Instance = this;
        }

        private void TboxSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TboxSearch.Text = string.Empty;
            StockInfo.Instance.TboxInfo.Text = string.Empty;
        }
        private void TboxSearch_OnKeyUp(object sender, KeyEventArgs e)
        {
            MainWindow.Instance.Dgrid1.ItemsSource = reader.ItemSearch(TboxSearch.Text).ToList();

        }
        private void CmbBoxViewByInnerItemType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbBoxViewByItemType.SelectedIndex == 0)
            {
                CmbBoxViewBySize.Items.Clear();
                switch (CmbBoxViewByInnerType.SelectedItem)
                {
                    case ClotheTypes.Shirt:
                        {
                            CmbBoxViewBySize.IsEnabled = true;
                            CmboBoxFiller.Fill(new ShirtSizeTypes(), CmbBoxViewBySize);
                            break;
                        }
                    default:
                        {
                            for (int i = 32; i <= 56; i++)
                            {
                                CmbBoxViewBySize.Items.Add(i);
                            }
                            break;
                        }
                }
            }
        }
        private void CmbBoxViewByItemType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CmbBoxViewByInnerType.Items.Clear();
            CmboBoxFiller.Fill(new ColorTypes(), CmbBoxViewByColor);
            CmbBoxViewByInnerType.IsEnabled = true;

            switch (CmbBoxViewByItemType.SelectedItem)
            {
                case ItemTypes.Clothe:
                    {
                        CmbBoxViewByColor.IsEnabled = true;
                        CmboBoxFiller.Fill(new ClotheTypes(), CmbBoxViewByInnerType);
                        break;
                    }
                case ItemTypes.Ball:
                    {
                        CmbBoxViewByColor.IsEnabled = true;
                        CmboBoxFiller.Fill(new BallTypes(), CmbBoxViewByInnerType);
                        break;
                    }
                case ItemTypes.Bat:
                    {
                        CmboBoxFiller.Fill(new BatTypes(), CmbBoxViewByInnerType);
                        break;
                    }
                case ItemTypes.Net:
                    {
                        CmboBoxFiller.Fill(new NetTypes(), CmbBoxViewByInnerType);
                        break;
                    }
            }
        }
        private void TboxMin_GotFocus(object sender, RoutedEventArgs e)
        {
            TboxStockMin.Text = string.Empty;
        }
        private void TboxMax_GotFocus(object sender, RoutedEventArgs e)
        {
            TboxStockMax.Text = string.Empty;
        }
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            bool byType = CmbBoxViewByItemType.Text != string.Empty;
            bool byInnerType = CmbBoxViewByInnerType.Text != string.Empty;
            bool byColor = CmbBoxViewByColor.Text != string.Empty;
            bool bySize = CmbBoxViewBySize.Text != string.Empty;
            
            string minPrice = TboxStockMin.Text == "Min" || TboxStockMin.Text == string.Empty ? "0" : TboxStockMin.Text;
            string maxPrice = TboxStockMax.Text == "Max" || TboxStockMax.Text == string.Empty ? 999999.ToString() : TboxStockMax.Text;

            
            
            if (byType)
            {
                MainWindow.Instance.Dgrid1.ItemsSource = reader.GetStock("ViewsByItemPrice",
                                                     CmbBoxViewByItemType.Text,
                                                     minPrice,
                                                     maxPrice).ToList();
            }
            if (byType && byInnerType)
            {
                MainWindow.Instance.Dgrid1.ItemsSource = reader.GetStock("ViewsByInnerItemPrice",
                                                     CmbBoxViewByItemType.Text,
                                                     CmbBoxViewByInnerType.Text,
                                                     minPrice,
                                                     maxPrice).ToList();
            }
            
            if (byType && byInnerType && byColor)
            {
                MainWindow.Instance.Dgrid1.ItemsSource = reader.GetStock("ViewsByColor",
                                                     CmbBoxViewByItemType.Text,
                                                     CmbBoxViewByInnerType.Text,
                                                     CmbBoxViewByColor.Text).ToList();
            }
            if (byType && byInnerType && bySize)
            {
                MainWindow.Instance.Dgrid1.ItemsSource = reader.GetStock("ViewsBySize",
                                                     CmbBoxViewByItemType.Text,
                                                     CmbBoxViewByInnerType.Text,
                                                     CmbBoxViewBySize.Text).ToList();
            }
            if (byType && byInnerType && byColor && bySize)
            {
                MainWindow.Instance.Dgrid1.ItemsSource = reader.GetStock("ViewsByAll",
                                                     CmbBoxViewByItemType.Text,
                                                     CmbBoxViewByInnerType.Text,
                                                     CmbBoxViewByColor.Text,
                                                     CmbBoxViewBySize.Text,
                                                     minPrice,
                                                     maxPrice).ToList();
            }
            return;
        }
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Dgrid1.ItemsSource = reader.GetTable().ToList();
        }
    }
}
