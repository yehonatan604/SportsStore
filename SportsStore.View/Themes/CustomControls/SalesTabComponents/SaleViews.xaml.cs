using System.Windows;
using System.Windows.Controls;
using System.Linq;
using SportsStore.Controller;
using SportsStore.Enums;
using SportsStore.Utilities;

namespace SportsStore.View.Themes.CustomControls.SalesTabComponents
{
    public partial class SaleViews : UserControl
    {
        private readonly Read read = MainWindow.Current.read;
        public static SaleViews? Current { get; private set; }
        public SaleViews()
        {
            InitializeComponent();
            Current = this;
        }
        private void TboxMin_GotFocus(object sender, RoutedEventArgs e)
        {
            TboxMin.Text = string.Empty;
        }
        private void TboxMax_GotFocus(object sender, RoutedEventArgs e)
        {
            TboxMax.Text = string.Empty;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dGrid = MainWindow.Current.Dgrid2;

            // check what is the search requirements
            bool byItemId = !string.IsNullOrEmpty(BoxById.Text);
            bool bySalesman = !string.IsNullOrEmpty(CmbBoxBySalesman.Text);
            bool byType = !string.IsNullOrEmpty(CmbBoxByItemType.Text);
            bool byInnerType = !string.IsNullOrEmpty(CmbBoxByInnerType.Text);
            bool byColor = !string.IsNullOrEmpty(CmbBoxByColor.Text);
            bool bySize = !string.IsNullOrEmpty(CmbBoxBySize.Text);
            bool byDate = !string.IsNullOrEmpty(CmbBoxByDate.Text);

            // if no price was given, search will be performed on values between min(0) & max(999,999)
            string minPrice = TboxMin.Text == "Min" || TboxMin.Text == string.Empty ? "0" : TboxMin.Text;
            string maxPrice = TboxMax.Text == "Max" || TboxMax.Text == string.Empty ? 999999.ToString() : TboxMax.Text;

            dGrid.ItemsSource = bySalesman ? read.GetSales
                                ("BySalseMan", CmbBoxBySalesman.Text, minPrice, maxPrice).ToList() :
                                byItemId ? read.GetSales
                                ("ByItemId", BoxById.Text, minPrice, maxPrice).ToList() :
                                byDate ? read.GetSales
                                ("ByDate", CmbBoxByDate.Text, minPrice, maxPrice).ToList() :
                                byType ? read.GetSales
                                ("ByType", CmbBoxByItemType.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType ? read.GetSales
                                ("ByTypeInnerType", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType && byDate ? read.GetSales
                                ("ByTypeInnerTypeDate", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, CmbBoxByDate.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType && bySalesman ? read.GetSales
                                ("ByTypeInnerTypeSalesman", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, CmbBoxBySalesman.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType && byColor ? read.GetSales
                                ("ByTypeInnerTypeColor", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, CmbBoxByColor.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType && bySize ? read.GetSales
                                ("ByTypeInnerTypeSize", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, CmbBoxBySize.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType && bySize && bySize ? read.GetSales
                                ("ByTypeInnerTypeColorSize", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, CmbBoxByColor.Text, CmbBoxBySize.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType && bySize && bySize && bySalesman ? read.GetSales
                                ("ByTypeInnerTypeColorSizeSalesman", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, CmbBoxByColor.Text, CmbBoxBySize.Text, CmbBoxBySalesman.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType && bySize && bySize && byDate ? read.GetSales
                                ("ByTypeInnerTypeColorSizeSalesman", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, CmbBoxByColor.Text, CmbBoxBySize.Text, CmbBoxByDate.Text, minPrice, maxPrice).ToList() :
                                // else by price only:
                                read.GetSales(minPrice, maxPrice).ToList();
        }

        private void CmbBoxByItemType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CmbBoxByInnerType.Items.Clear();
            CmboBoxFiller.Fill(new ColorTypes(), CmbBoxByColor);

            switch (CmbBoxByItemType.SelectedItem)
            {
                case ItemTypes.Clothe: { CmboBoxFiller.Fill(new ClotheTypes(), CmbBoxByInnerType); break; }
                case ItemTypes.Ball: { CmboBoxFiller.Fill(new BallTypes(), CmbBoxByInnerType); break; }
                case ItemTypes.Bat: { CmboBoxFiller.Fill(new BatTypes(), CmbBoxByInnerType); break; }
                case ItemTypes.Net: { CmboBoxFiller.Fill(new NetTypes(), CmbBoxByInnerType); break; }
            }
        }

        private void CmbBoxByInnerType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbBoxByItemType.SelectedItem is ItemTypes.Clothe)
            {
                CmbBoxBySize.Items.Clear();
                switch (CmbBoxByInnerType.SelectedItem)
                {
                    case ClotheTypes.Shirt:
                        {
                            CmbBoxBySize.IsEnabled = true;
                            CmboBoxFiller.Fill(new ShirtSizeTypes(), CmbBoxBySize);
                            break;
                        }
                    default:
                        {
                            for (int i = 32; i <= 56; i++)
                            {
                                CmbBoxBySize.Items.Add(i);
                            }
                            break;
                        }
                }
            }
        }
    }
}
