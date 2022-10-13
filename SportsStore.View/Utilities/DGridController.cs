using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using SportsStore.View;
using System.Windows.Input;
using SportsStore.Controller;
using Microsoft.Extensions.Logging;
using SportsStore.Enums;

namespace SportsStore.Utilities
{
    public static class DGridController
    {
        // DataGrids Global Methods

        private static List<DataGrid> Dgrids;

        public static void OnStart(List<DataGrid> grids)
        {
            Dgrids = grids;
            RefreshDgrid();
            DgridController(grids[0]);
        }
        public static void DgridController(DataGrid currentGrid)
        {
            foreach (var control in MainWindow.Current.StockTabGrid.Children)
            {
                if (control is DataGrid grid)
                {
                    grid.Visibility = Visibility.Collapsed;
                }
            }
            currentGrid.Visibility = Visibility.Visible;
        }
        public static void RefreshDgrid(DataGrid? dGrid = null)
        {
            if (dGrid == null)
            {
                Dgrids[0].ItemsSource = new StockEntityHandler().GetTable("Stock").ToList();
                Dgrids[1].ItemsSource = new StockEntityHandler().GetTable("Stock").ToList();
            }
            if (dGrid == Dgrids[2])
            {
                Dgrids[2].ItemsSource = new SaleEntityHandler().GetTable("Sales").ToList();
            }
            if (dGrid == Dgrids[3])
            {
                Dgrids[3].ItemsSource = new CustomerEntityHandler().GetTable("Customers").ToList();
            }
            if (dGrid == Dgrids[4])
            {
                Dgrids[4].ItemsSource = new UserEntityHandler().GetTable("Users").ToList();
            }
            if (dGrid == Dgrids[5])
            {
                Dgrids[5].ItemsSource = new LogEntityHandler().GetTable("Logs").ToList();
            }
        }
        public static string GetDgridContent(DataGrid dGrid, int cell = 0)
        {
            try
            {
                return ((TextBlock)dGrid.SelectedCells[cell].Column.GetCellContent(dGrid.SelectedCells[cell].Item)).Text;

            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
