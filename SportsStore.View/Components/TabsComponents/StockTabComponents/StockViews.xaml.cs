using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using SportsStore.Controller;
using SportsStore.Enums;
using SportsStore.Utilities;

namespace SportsStore.View.Themes.CustomControls
{
    public partial class StockViews : UserControl
    {
        public static StockViews? Current { get; private set; }
        StockEntityHandler handler;
        public StockViews()
        {
            InitializeComponent();
            Current = this;
            handler = new();
        }

        private void TboxSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TboxSearch.Text = string.Empty;
            StockInfo.Current.TboxInfo.Text = string.Empty;
        }
        private void TboxSearch_OnKeyUp(object sender, KeyEventArgs e)
        {
            MainWindow.Current.Dgrid1.ItemsSource = handler.Search("", TboxSearch.Text).ToList();

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
                MainWindow.Current.Dgrid1.ItemsSource = handler.GetTable("ByPrice",
                                                        CmbBoxViewByItemType.Text,
                                                        minPrice,
                                                        maxPrice).ToList();
            }
            if (byType && byInnerType)
            {
                MainWindow.Current.Dgrid1.ItemsSource = handler.GetTable("ByInnerItem_Price",
                                                        CmbBoxViewByItemType.Text,
                                                        CmbBoxViewByInnerType.Text,
                                                        minPrice,
                                                        maxPrice).ToList();
            }

            if (byType && byInnerType && byColor)
            {
                MainWindow.Current.Dgrid1.ItemsSource = handler.GetTable("ByColor",
                                                        CmbBoxViewByItemType.Text,
                                                        CmbBoxViewByInnerType.Text,
                                                        CmbBoxViewByColor.Text).ToList();
            }
            if (byType && byInnerType && bySize)
            {
                MainWindow.Current.Dgrid1.ItemsSource = handler.GetTable("BySize",
                                                        CmbBoxViewByItemType.Text,
                                                        CmbBoxViewByInnerType.Text,
                                                        CmbBoxViewBySize.Text).ToList();
            }
            if (byType && byInnerType && byColor && bySize)
            {
                MainWindow.Current.Dgrid1.ItemsSource = handler.GetTable("ByAll",
                                                        CmbBoxViewByItemType.Text,
                                                        CmbBoxViewByInnerType.Text,
                                                        CmbBoxViewByColor.Text,
                                                        CmbBoxViewBySize.Text,
                                                        minPrice,
                                                        maxPrice).ToList();
            }
            handler.AddSearchLog();
            return;
        }
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Current.Dgrid1.ItemsSource = handler.GetTable("Stock").ToList();
        }
    }
}
