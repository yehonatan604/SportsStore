using SportsStore.Controller;
using SportsStore.Enums;
using SportsStore.View.Themes.CustomControls.EditStockTabComponents;
using SportsStore.View.Themes.CustomControls.SalesTabComponents;
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
            Read reader = new();

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

    }
}
