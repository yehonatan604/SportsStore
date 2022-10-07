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
        private readonly Read read = MainWindow.Current.read;
        public static StockViews? Current { get; private set; }
        public StockViews()
        {
            InitializeComponent();
            Current = this;
        }

        private void TboxSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TboxSearch.Text = string.Empty;
            StockInfo.Current.TboxInfo.Text = string.Empty;
        }
        private void TboxSearch_OnKeyUp(object sender, KeyEventArgs e)
        {
            MainWindow.Current.Dgrid1.ItemsSource = read.ItemSearch(TboxSearch.Text).ToList();

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
                MainWindow.Current.Dgrid1.ItemsSource = read.GetStock("ViewsByItemPrice",
                                                     CmbBoxViewByItemType.Text,
                                                     minPrice,
                                                     maxPrice).ToList();
            }
            if (byType && byInnerType)
            {
                MainWindow.Current.Dgrid1.ItemsSource = read.GetStock("ViewsByInnerItemPrice",
                                                     CmbBoxViewByItemType.Text,
                                                     CmbBoxViewByInnerType.Text,
                                                     minPrice,
                                                     maxPrice).ToList();
            }
            
            if (byType && byInnerType && byColor)
            {
                MainWindow.Current.Dgrid1.ItemsSource = read.GetStock("ViewsByColor",
                                                     CmbBoxViewByItemType.Text,
                                                     CmbBoxViewByInnerType.Text,
                                                     CmbBoxViewByColor.Text).ToList();
            }
            if (byType && byInnerType && bySize)
            {
                MainWindow.Current.Dgrid1.ItemsSource = read.GetStock("ViewsBySize",
                                                     CmbBoxViewByItemType.Text,
                                                     CmbBoxViewByInnerType.Text,
                                                     CmbBoxViewBySize.Text).ToList();
            }
            if (byType && byInnerType && byColor && bySize)
            {
                MainWindow.Current.Dgrid1.ItemsSource = read.GetStock("ViewsByAll",
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
            MainWindow.Current.Dgrid1.ItemsSource = read.GetTable().ToList();
        }
    }
}
