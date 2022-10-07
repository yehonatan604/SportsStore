using SportsStore.Controller;
using SportsStore.Enums;
using SportsStore.View.Themes.CustomControls.EditStockTabComponents;
using SportsStore.View.Themes.CustomControls.LogsTabComponents;
using SportsStore.View.Themes.CustomControls.SalesTabComponents;
using SportsStore.View.Themes.CustomControls.UsersTabComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SportsStore.View.Utilities
{
    public class CmboBoxFiller
    {
        private static readonly Read reader = new();
        public static void Fill(Enum sender, ComboBox box)
        {
            foreach (var item in Enum.GetValues(sender.GetType()))
            {
                box.Items.Add(item);
            }
        }

        public static void FillSizeTypes(ClotheTypes type)
        {
            EditStockAddItem.Instance.BoxSize.Items.Clear();
            EditStockEditItem.Instance.BoxSize.Items.Clear();
            switch (type)
            {
                case ClotheTypes.Shirt:
                    {
                        foreach (var item in Enum.GetValues(typeof(ShirtSizeTypes)))
                        {
                            EditStockAddItem.Instance.BoxSize.Items.Add(item);
                            EditStockEditItem.Instance.BoxSize.Items.Add(item);
                        }
                        break;
                    }
                default:
                    {
                        for (int i = 26; i <= 52; i++)
                        {
                            EditStockAddItem.Instance.BoxSize.Items.Add(i);
                            EditStockEditItem.Instance.BoxSize.Items.Add(i);
                        }
                        break;
                    }
            }
        }

        public static void FillSalesBoxes()
        {
            foreach (string str in reader.GetList("ByItem"))
            {
                SaleViews.Instance.CmbBoxByItemType.Items.Add(str);
            }
            foreach (string str in reader.GetList("BySalesMan"))
            {
                SaleViews.Instance.CmbBoxBySalesman.Items.Add(str);
            }
            foreach (string str in reader.GetList("ByDate"))
            {
                SaleViews.Instance.CmbBoxByDate.Items.Add(str);
            }
        }
        public static void FillUserBoxes()
        {
            Fill(new UserTypes(), UsersView.Instance.BoxByUserType);

            foreach (string date in reader.GetList("ByDate"))
            {
                UsersView.Instance.BoxBySaleDate.Items.Add(date);
            }
            foreach (string date in reader.GetList("ByHireDate"))
            {
                UsersView.Instance.BoxByHireYear.Items.Add(date);
            }
        }
        public static void FillLogsBoxes()
        {
            Fill(new UserTypes(), LogsView.Instance.BoxByUserType);

            foreach (string str in reader.GetList("ByLogAction"))
            {
                LogsView.Instance.BoxByActionType.Items.Add(str);
            }
            foreach (string str in reader.GetList("ByLogDate"))
            {
                LogsView.Instance.BoxByActionDate.Items.Add(str);
            }
        }

    }
}
