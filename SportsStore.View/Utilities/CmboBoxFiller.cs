using System;
using System.Windows.Controls;
using SportsStore.Enums;
using SportsStore.Controller;
using SportsStore.View.Themes.CustomControls;
using SportsStore.View.Themes.CustomControls.EditStockTabComponents;
using SportsStore.View.Themes.CustomControls.LogsTabComponents;
using SportsStore.View.Themes.CustomControls.SalesTabComponents;
using SportsStore.View.Themes.CustomControls.UsersTabComponents;

namespace SportsStore.Utilities
{
    public static class CmboBoxFiller
    {
        private static readonly Read reader = new();
        public static void OnStart()
        {
            Fill(new ItemTypes(), StockViews.Current.CmbBoxViewByItemType);
            Fill(new ItemTypes(), EditStockAddItem.Current.BoxItemType);
            Fill(new ItemTypes(), EditStockEditItem.Current.BoxItemType);
            Fill(new ItemTypes(), SaleViews.Current.CmbBoxByItemType);
            Fill(new ColorTypes(), EditStockAddItem.Current.BoxColor);
            Fill(new ColorTypes(), EditStockEditItem.Current.BoxColor);
            FillSalesBoxes();
            FillUserBoxes();
            FillLogsBoxes();
        }
        public static void Fill(Enum sender, ComboBox box)
        {
            foreach (var item in Enum.GetValues(sender.GetType()))
            {
                box.Items.Add(item);
            }
        }

        public static void FillSizeTypes(ClotheTypes type)
        {
            EditStockAddItem.Current.BoxSize.Items.Clear();
            EditStockEditItem.Current.BoxSize.Items.Clear();
            switch (type)
            {
                case ClotheTypes.Shirt:
                    {
                        foreach (var item in Enum.GetValues(typeof(ShirtSizeTypes)))
                        {
                            EditStockAddItem.Current.BoxSize.Items.Add(item);
                            EditStockEditItem.Current.BoxSize.Items.Add(item);
                        }
                        break;
                    }
                default:
                    {
                        for (int i = 26; i <= 52; i++)
                        {
                            EditStockAddItem.Current.BoxSize.Items.Add(i);
                            EditStockEditItem.Current.BoxSize.Items.Add(i);
                        }
                        break;
                    }
            }
        }

        public static void FillSalesBoxes()
        {
            foreach (string str in reader.GetList("ByItem"))
            {
                SaleViews.Current.CmbBoxByItemType.Items.Add(str);
            }
            foreach (string str in reader.GetList("BySalesMan"))
            {
                SaleViews.Current.CmbBoxBySalesman.Items.Add(str);
            }
            foreach (string str in reader.GetList("ByDate"))
            {
                SaleViews.Current.CmbBoxByDate.Items.Add(str);
            }
        }
        public static void FillUserBoxes()
        {
            Fill(new UserTypes(), UsersView.Current.BoxByUserType);

            foreach (string date in reader.GetList("ByDate"))
            {
                UsersView.Current.BoxBySaleDate.Items.Add(date);
            }
            foreach (string date in reader.GetList("ByHireDate"))
            {
                UsersView.Current.BoxByHireYear.Items.Add(date);
            }
        }
        public static void FillLogsBoxes()
        {
            Fill(new UserTypes(), LogsView.Current.BoxByUserType);

            foreach (string str in reader.GetList("ByLogAction"))
            {
                LogsView.Current.BoxByActionType.Items.Add(str);
            }
            foreach (string str in reader.GetList("ByLogDate"))
            {
                LogsView.Current.BoxByActionDate.Items.Add(str);
            }
        }

    }
}
