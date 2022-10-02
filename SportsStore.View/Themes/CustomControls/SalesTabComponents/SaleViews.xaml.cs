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

namespace SportsStore.View.Themes.CustomControls.SalesTabComponents
{
    /// <summary>
    /// Interaction logic for SaleViews.xaml
    /// </summary>
    public partial class SaleViews : UserControl
    {
        Read reader;
        public static SaleViews Instance;
        public SaleViews()
        {
            InitializeComponent();
            Instance = this;
            reader = new();
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
            DataGrid dGrid = MainWindow.Instance.Dgrid2;

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

            dGrid.ItemsSource = bySalesman ? reader.GetSales
                                ("BySalseMan", CmbBoxBySalesman.Text, minPrice, maxPrice).ToList() :
                                byItemId ? reader.GetSales
                                ("ByItemId", BoxById.Text, minPrice, maxPrice).ToList() :
                                byDate ? reader.GetSales
                                ("ByDate", CmbBoxByDate.Text, minPrice, maxPrice).ToList() :
                                byType ? reader.GetSales
                                ("ByType", CmbBoxByItemType.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType ? reader.GetSales
                                ("ByTypeInnerType", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType && byDate ? reader.GetSales
                                ("ByTypeInnerTypeDate", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, CmbBoxByDate.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType && bySalesman ? reader.GetSales
                                ("ByTypeInnerTypeSalesman", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, CmbBoxBySalesman.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType && byColor ? reader.GetSales
                                ("ByTypeInnerTypeColor", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, CmbBoxByColor.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType && bySize ? reader.GetSales
                                ("ByTypeInnerTypeSize", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, CmbBoxBySize.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType && bySize && bySize ? reader.GetSales
                                ("ByTypeInnerTypeColorSize", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, CmbBoxByColor.Text, CmbBoxBySize.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType && bySize && bySize && bySalesman ? reader.GetSales
                                ("ByTypeInnerTypeColorSizeSalesman", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, CmbBoxByColor.Text, CmbBoxBySize.Text, CmbBoxBySalesman.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType && bySize && bySize && byDate ? reader.GetSales
                                ("ByTypeInnerTypeColorSizeSalesman", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, CmbBoxByColor.Text, CmbBoxBySize.Text, CmbBoxByDate.Text, minPrice, maxPrice).ToList() :
                                // else by price only:
                                reader.GetSales(minPrice, maxPrice).ToList();
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
            if (CmbBoxByItemType.SelectedIndex == 0)
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
