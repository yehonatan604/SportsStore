using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using SportsStore.Controller;
using SportsStore.Utilities;
using SportsStore.View.Themes.CustomControls;
using SportsStore.View.Themes.CustomControls.EditStockTabComponents;
using SportsStore.View.Themes.CustomControls.SalesTabComponents;
using SportsStore.View.Themes.CustomControls.CostumerTabCComponents;
using SportsStore.View.Themes.CustomControls.UsersTabComponents;
using SportsStore.View.Themes.CustomControls.LogsTabComponents;
using System.Collections.Generic;

namespace SportsStore.View
{
    public partial class MainWindow : Window
    {
        // Fields
        public readonly Create write;
        public readonly Read read;
        public readonly Update update;
        public readonly Delete delete;
        public static MainWindow? Current { get; private set; }

        // Constructor
        public MainWindow()
        {
            Current = this;
            read = new();
            update = new();
            delete = new();
            write = new();
            InitializeComponent();
            write.OnStartProgram();

            if (!DbCrud.LoggedInUser.RememberMe)
            {
                LogInWindow loginWindow = new();
                loginWindow.ShowDialog();
            }

            DGridController.OnStart(new List<DataGrid>() { Dgrid1, Dgrid1_1, Dgrid2, Dgrid3, Dgrid4, Dgrid5 });
            CmboBoxFiller.OnStart();
            Clock.Start(LblTime);
        }

        // Stock_Tab Event Handlers
        private void Stocks_Tabs_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DGridController.RefreshDgrid();
            DGridController.DgridController(Dgrid1);
        }
        private void DGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dGrid)
            {
                StockInfo.Current.TboxInfo.Text = $"{DGridController.GetDgridContent(dGrid, 5)}\n" +
                                $"Id: {DGridController.GetDgridContent(dGrid, 4)}\n" +
                                $"Price: {DGridController.GetDgridContent(dGrid, 8)}\n" +
                                $"Color: {DGridController.GetDgridContent(dGrid, 9)}\n" +
                                $"Size: {DGridController.GetDgridContent(dGrid, 10)}\n" +
                                $"In Stock: {DGridController.GetDgridContent(dGrid, 2)}\n";

                StockInfo.Current.TboxInfo.Text += $"\n\n{DGridController.GetDgridContent(dGrid, 5)}\n" +
                                       $"is a {DGridController.GetDgridContent(dGrid, 6)} {DGridController.GetDgridContent(dGrid, 7)} " +
                                       $"and it costs {DGridController.GetDgridContent(dGrid, 8)} Shekels.\n" +
                                       $"we are working with this product\nsince: {DGridController.GetDgridContent(dGrid, 11)}";

                StockSell.Current.TboxSellID.Text = DGridController.GetDgridContent(dGrid, 4);
            }
        }

        // Customers_Tab Event Handlers
        private void Customers_Tab_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DGridController.RefreshDgrid(Dgrid3);
            DGridController.DgridController(Dgrid3);
            CustomerStats.Current.CollectCharts();
        }
        private void Dgrid3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CustomersInfo.Current.TboxInfo.Text = $"Customer: {DGridController.GetDgridContent(Dgrid3, 1)} {DGridController.GetDgridContent(Dgrid3, 2)}\n" +
                                    $"id: {DGridController.GetDgridContent(Dgrid3)}\n" +
                                    $"Email: {DGridController.GetDgridContent(Dgrid3, 3)}\n\n" +
                                    $"Purchases Count: {DGridController.GetDgridContent(Dgrid3, 7)}\n" +
                                    $"Total Purchases: {float.Parse(DGridController.GetDgridContent(Dgrid3, 6))}$\n" +
                                    $"Last Purchase at: {DGridController.GetDgridContent(Dgrid3, 9)}\n";
            }
            catch
            {
                DGridController.RefreshDgrid(Dgrid3);
            }
        }

        // EditStock_Tab Event Handlers
        private void EditStock_Tab_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DGridController.RefreshDgrid();
            DGridController.DgridController(Dgrid1_1);
        }
        private void DGrid1_1SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dGrid)
            {
                EditStockInfo.Current.TboxInfo.Text = $"{DGridController.GetDgridContent(dGrid, 5)}\n" +
                                $"Id: {DGridController.GetDgridContent(dGrid, 4)}\n" +
                                $"Price: {DGridController.GetDgridContent(dGrid, 8)}\n" +
                                $"Color: {DGridController.GetDgridContent(dGrid, 9)}\n" +
                                $"Size: {DGridController.GetDgridContent(dGrid, 10)}\n" +
                                $"In Stock: {DGridController.GetDgridContent(dGrid, 2)}\n";

                EditStockInfo.Current.TboxInfo.Text += $"\n\n{DGridController.GetDgridContent(dGrid, 5)}\n" +
                                       $"is a {DGridController.GetDgridContent(dGrid, 7)} {DGridController.GetDgridContent(dGrid, 6)} " +
                                       $"and it costs {DGridController.GetDgridContent(dGrid, 8)} Shekels.\n" +
                                       $"we are working with this product\nsince: {DGridController.GetDgridContent(dGrid, 11)}";

                EditStockAddItem.Current.BoxId.Text = DGridController.GetDgridContent(dGrid, 4);

                EditStockEditItem.Current.BoxItemId.Text = DGridController.GetDgridContent(dGrid, 4);
                EditStockEditItem.Current.BoxItemName.Text = DGridController.GetDgridContent(dGrid, 5);
                EditStockEditItem.Current.BoxItemPrice.Text = DGridController.GetDgridContent(dGrid, 8);
                EditStockEditItem.Current.BoxQuantity.Text = DGridController.GetDgridContent(dGrid, 2);
                EditStockEditItem.Current.BoxItemType.Text = DGridController.GetDgridContent(dGrid, 6);
                EditStockEditItem.Current.BoxItemInnerType.Text = DGridController.GetDgridContent(dGrid, 7);
                EditStockEditItem.Current.BoxSize.Text = DGridController.GetDgridContent(dGrid, 10);
                EditStockEditItem.Current.BoxColor.Text = DGridController.GetDgridContent(dGrid, 9);
            }
        }

        // Sales_Tab Event Handlers
        private void Sales_Tab_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DGridController.RefreshDgrid(Dgrid2);
            DGridController.DgridController(Dgrid2);
            SalesStats.Current.CollectCharts();
        }
        private void Dgrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaleInfo.Current.TboxInfo.Text = $"Sale Id: {DGridController.GetDgridContent(Dgrid2)}\n\n" +
                                              $"Item: {DGridController.GetDgridContent(Dgrid2, 5)}, Id: {DGridController.GetDgridContent(Dgrid2, 4)}\n" +
                                              $"Type: {DGridController.GetDgridContent(Dgrid2, 7)} {DGridController.GetDgridContent(Dgrid2, 6)}\n\n" +
                                              $"{DGridController.GetDgridContent(Dgrid2, 2)} units were soled for total of {DGridController.GetDgridContent(Dgrid2, 3)}\n" +
                                              $"at {DGridController.GetDgridContent(Dgrid2, 1)}\n" +
                                              $"by: {DGridController.GetDgridContent(Dgrid2, 12)} {DGridController.GetDgridContent(Dgrid2, 13)}, Id: {DGridController.GetDgridContent(Dgrid2, 11)}";
        }

        // Users_Tab Event Handlers
        private void Users_Tab_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DGridController.RefreshDgrid(Dgrid4);
            DGridController.DgridController(Dgrid4);
        }
        private void Dgrid4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (DGridController.GetDgridContent(Dgrid4) != "")
                {
                    UsersInfo.Current.TboxInfo.Text =
                        $"{DGridController.GetDgridContent(Dgrid4, 1)} {DGridController.GetDgridContent(Dgrid4, 2)}\n" +
                        $"id: {DGridController.GetDgridContent(Dgrid4)}\n" +
                        $"User Type: {DGridController.GetDgridContent(Dgrid4, 4)}\n" +
                        $"Email: {DGridController.GetDgridContent(Dgrid4, 5)}\n" +
                        $"Hire Date: {DGridController.GetDgridContent(Dgrid4, 3)}\n\n" +
                        $"Last Sale: {DGridController.GetDgridContent(Dgrid4, 8)}\n" +
                        $"Sales Total: {DGridController.GetDgridContent(Dgrid4, 9)}\n" +
                        $"Sales Count: {DGridController.GetDgridContent(Dgrid4, 10)}";

                    UsersEdit.Current.BoxUserId.Text = DGridController.GetDgridContent(Dgrid4);
                }
            }
            catch
            {
                DGridController.RefreshDgrid(Dgrid4);
            }
        }

        // Logs_Tab Event Handlers
        private void Logs_Tab_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DGridController.RefreshDgrid(Dgrid5);
            DGridController.DgridController(Dgrid5);
        }
        private void Dgrid5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (DGridController.GetDgridContent(Dgrid5) != "")
                {
                    LogsInfo.Current.TboxInfo.Text =
                        $"{DGridController.GetDgridContent(Dgrid5, 1)} {DGridController.GetDgridContent(Dgrid5, 2)}\n" +
                        $"id: {DGridController.GetDgridContent(Dgrid5)}\n" +
                        $"User Type: {DGridController.GetDgridContent(Dgrid5, 4)}\n" +
                        $"Email: {DGridController.GetDgridContent(Dgrid5, 5)}\n" +
                        $"Hire Date: {DGridController.GetDgridContent(Dgrid5, 3)}\n\n";
                }
            }
            catch
            {
                DGridController.RefreshDgrid(Dgrid5);
            }
        }

        // Global Tab Headers Event Handlers
        private void TabHdrStock_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is TextBlock tb)
            {
                tb.Foreground = new SolidColorBrush(Colors.Wheat);
            }
        }
        private void TabHdrStock_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is TextBlock tb)
            {
                tb.Foreground = new SolidColorBrush(Colors.LemonChiffon);
            }
        }

        // OnExit Event Handler
        private void OnExit(object sender, CancelEventArgs e)
        {
            write.OnExitProgram();
        }
    }
}