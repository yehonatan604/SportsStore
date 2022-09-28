using SportsStore.Controller;
using SportsStore.Enums;
using SportsStore.View.Utilities;
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
            MainWindow.Instance.Dgrid1.ItemsSource = reader.Search(TboxSearch.Text).ToList();

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
            MainWindow.Instance.TboxMin.Text = string.Empty;
        }
        private void TboxMax_GotFocus(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.TboxMax.Text = string.Empty;
        }
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            bool byType = CmbBoxViewByItemType.Text != string.Empty;
            bool byInnerType = CmbBoxViewByInnerType.Text != string.Empty;
            bool byColor = CmbBoxViewByColor.Text != string.Empty;
            bool bySize = CmbBoxViewBySize.Text != string.Empty;
            bool byPriceMin = TboxStockMin.Text != string.Empty && TboxStockMin.Text != "Min";
            bool byPriceMax = TboxStockMin.Text != string.Empty && TboxStockMin.Text != "Max";

            if (byType)
            {
                MainWindow.Instance.Dgrid1.ItemsSource = reader.GetTable("ViewsByType",
                                                     CmbBoxViewByItemType.Text).ToList();
            }
            if (byPriceMin && byPriceMax)
            {
                MainWindow.Instance.Dgrid1.ItemsSource = reader.GetTable("ViewsByPrice",
                                                     TboxStockMin.Text,
                                                     TboxStockMax.Text).ToList();
            }
            if (byType && byPriceMin && byPriceMax)
            {
                MainWindow.Instance.Dgrid1.ItemsSource = reader.GetTable("ViewsByItemPrice",
                                                     CmbBoxViewByItemType.Text,
                                                     TboxStockMin.Text,
                                                     TboxStockMax.Text).ToList();
            }
            if (byType && byInnerType && byPriceMin && byPriceMax)
            {
                MainWindow.Instance.Dgrid1.ItemsSource = reader.GetTable("ViewsByItemPrice",
                                                     CmbBoxViewByItemType.Text,
                                                     CmbBoxViewByInnerType.Text,
                                                     TboxStockMin.Text,
                                                     TboxStockMax.Text).ToList();
            }
            if (byType && byInnerType)
            {
                MainWindow.Instance.Dgrid1.ItemsSource = reader.GetTable("ViewsByInnerType",
                                                     CmbBoxViewByItemType.Text,
                                                     CmbBoxViewByInnerType.Text).ToList();
            }
            if (byType && byInnerType && byColor)
            {
                MainWindow.Instance.Dgrid1.ItemsSource = reader.GetTable("ViewsByColor",
                                                     CmbBoxViewByItemType.Text,
                                                     CmbBoxViewByInnerType.Text,
                                                     CmbBoxViewByColor.Text).ToList();
            }
            if (byType && byInnerType && bySize)
            {
                MainWindow.Instance.Dgrid1.ItemsSource = reader.GetTable("ViewsBySize",
                                                     CmbBoxViewByItemType.Text,
                                                     CmbBoxViewByInnerType.Text,
                                                     CmbBoxViewBySize.Text).ToList();
            }
            if (byType && byInnerType && byColor && bySize)
            {
                MainWindow.Instance.Dgrid1.ItemsSource = reader.GetTable("ViewsByAllNoPrice",
                                                     CmbBoxViewByItemType.Text,
                                                     CmbBoxViewByInnerType.Text,
                                                     CmbBoxViewByColor.Text,
                                                     CmbBoxViewBySize.Text).ToList();
            }
            if (byType && byInnerType && byColor && bySize && byPriceMin && byPriceMax)
            {
                MainWindow.Instance.Dgrid1.ItemsSource = reader.GetTable("ViewsByAllNoPrice",
                                                     CmbBoxViewByItemType.Text,
                                                     CmbBoxViewByInnerType.Text,
                                                     CmbBoxViewByColor.Text,
                                                     CmbBoxViewBySize.Text,
                                                     TboxStockMin.Text,
                                                     TboxStockMax.Text).ToList();
            }
            return;
        }
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Dgrid1.ItemsSource = reader.GetTable().ToList();
        }
    }
}
