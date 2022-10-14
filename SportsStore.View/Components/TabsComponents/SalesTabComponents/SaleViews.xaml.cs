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
        public static SaleViews? Current { get; private set; }
        private readonly SaleEntityHandler handler;
        public SaleViews()
        {
            InitializeComponent();
            Current = this;
            handler = new();
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

            dGrid.ItemsSource = bySalesman ? handler.GetTable
                                ("BySalseMan", CmbBoxBySalesman.Text, minPrice, maxPrice).ToList() :
                                byItemId ? handler.GetTable
                                ("ByItemId", BoxById.Text, minPrice, maxPrice).ToList() :
                                byDate ? handler.GetTable
                                ("ByDate", CmbBoxByDate.Text, minPrice, maxPrice).ToList() :
                                byType ? handler.GetTable
                                ("ByType", CmbBoxByItemType.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType ? handler.GetTable
                                ("ByType_InnerType", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType && byDate ? handler.GetTable
                                ("ByType_InnerType_Date", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, CmbBoxByDate.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType && bySalesman ? handler.GetTable
                                ("ByType_InnerType_Salesman", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, CmbBoxBySalesman.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType && byColor ? handler.GetTable
                                ("ByType_InnerType_Color", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, CmbBoxByColor.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType && bySize ? handler.GetTable
                                ("ByType_InnerType_Size", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, CmbBoxBySize.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType && bySize && bySize ? handler.GetTable
                                ("ByType_InnerType_Color_Size", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, CmbBoxByColor.Text, CmbBoxBySize.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType && bySize && bySize && bySalesman ? handler.GetTable
                                ("ByType_InnerType_Color_Size_Salesman", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, CmbBoxByColor.Text, CmbBoxBySize.Text, CmbBoxBySalesman.Text, minPrice, maxPrice).ToList() :
                                byType && byInnerType && bySize && bySize && byDate ? handler.GetTable
                                ("ByType_InnerType_Color_Size_date", CmbBoxByItemType.Text, CmbBoxByInnerType.Text, CmbBoxByColor.Text, CmbBoxBySize.Text, CmbBoxByDate.Text, minPrice, maxPrice).ToList() :
                                // else by price only:
                                handler.GetTable("Default", minPrice, maxPrice).ToList();

            handler.AddSearchLog();
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

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            BoxById.Text = string.Empty;
            CmbBoxByItemType.Text = string.Empty;
            CmbBoxByInnerType.Text = string.Empty;
            CmbBoxByColor.Text = string.Empty;
            CmbBoxBySize.Text = string.Empty;
            CmbBoxBySalesman.Text = string.Empty;
            CmbBoxByDate.Text = string.Empty;
            TboxMin.Text = string.Empty;
            TboxMax.Text = string.Empty;

            DGridController.RefreshDgrid(MainWindow.Current.Dgrid2);
        }
    }
}
